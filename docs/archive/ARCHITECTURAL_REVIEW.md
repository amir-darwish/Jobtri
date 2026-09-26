# Jobtri — Architectural Review & System Audit

**Date:** 2026-09-24  
**Role:** Senior .NET Architect & Backend Engineer  
**Status:** Baseline Established  

---

## 1. Executive Summary & Project Vision

**Jobtri** is not a simple job board or web scraper. It is a **Job Search Decision Support System** designed to answer the core question:
> *"Among thousands of available jobs, which ones are actually worth my time, and why?"*

### End-to-End Pipeline
```text
Company Discovery
        ↓
ATS Detection
        ↓
Job Acquisition (IAtsConnector)
        ↓
Normalization
        ↓
Deduplication (CanonicalJobId)
        ↓
Database Storage (PostgreSQL)
        ↓
Hard Filters
        ↓
Deterministic Scoring
        ↓
AI Analysis (Top Candidates Only)
        ↓
Human Review
        ↓
Application Tracking
        ↓
Learning Engine
```

### Technology Decisions & Architectural Constraints
* **Platform:** .NET 10 LTS / C# / ASP.NET Core
* **Persistence:** PostgreSQL via Docker Compose (`postgres:17`), Entity Framework Core (Npgsql)
* **Configuration & Secrets:** User Secrets for local .NET development, `.env` for Docker
* **Architecture Style:** Clean Architecture / Modular Monolith (`Jobtri.Domain`, `Jobtri.Application`, `Jobtri.Infrastructure`, `Jobtri.Api`)
* **Strict Simplicity Rule:** Intentionally avoid MediatR, CQRS, generic repositories, or premature microservices. Prefer clean, feature-oriented application services.
* **Conventions:**
  * Primary keys use `int` IDs.
  * Enums are prefixed with `en` (e.g. `enAtsType`).
  * Signatures kept concise and single-line where practical.

---

## 2. Current Architecture & Implementation Map

```text
src/
├── Jobtri.Domain/                  # Pure enterprise domain models (Zero external dependencies)
│   ├── Common/Guard.cs             # Invariant checks & URI validators
│   ├── Entities/Company.cs         # Hiring organization entity
│   ├── Entities/CompanySource.cs   # Career/ATS source per company
│   ├── Entities/Job.cs             # Core opportunity entity (in-progress)
│   └── Enums/enAtsType.cs          # ATS provider enumeration
│
├── Jobtri.Application/             # Business use cases, services & interfaces
│   ├── Abstractions/
│   │   ├── Integrations/
│   │   │   └── IAtsConnector.cs    # Connector contract for ATS ingest
│   │   └── Persistence/
│   │       ├── ICompanyRepository.cs
│   │       ├── ICompanySourceRepository.cs
│   │       └── IJobRepository.cs
│   ├── Companies/CompanyService.cs
│   ├── CompanySources/CompanySourceService.cs
│   ├── Jobs/JobService.cs
│   └── DependencyInjection.cs
│
├── Jobtri.Infrastructure/          # External concerns & database persistence
│   ├── Persistence/
│   │   ├── JobtriDbContext.cs
│   │   ├── Configurations/         # Fluent API entity configurations
│   │   │   ├── CompanyConfiguration.cs
│   │   │   ├── CompanySourceConfiguration.cs
│   │   │   └── JobConfiguration.cs
│   │   ├── Migrations/
│   │   │   └── 20260920213105_InitialCreate.cs
│   │   └── Repositories/
│   │       ├── CompanyRepository.cs
│   │       ├── CompanySourceRepository.cs
│   │       └── JobRepository.cs
│   └── DependencyInjection.cs
│
└── Jobtri.Api/                     # HTTP delivery layer & controllers
    ├── Controllers/
    │   ├── CompaniesController.cs
    │   ├── CompanySourcesController .cs   # (Contains trailing space in filename)
    │   └── JobsController.cs
    ├── Program.cs
    └── appsettings.json
```

---

## 3. Discovered Technical Issues & Architectural Risks

During code and schema review, the following critical issues were identified:

### 1. Database Schema Desynchronization (High Priority)
* **Problem:** Migration `20260920213105_InitialCreate` created the `jobs` table with: `Id`, `Title`, `CompanySourceId`, `SourceJobId`, `JobUrl`, `Location`, `DatePosted`, `FirstSeenAt`.
* However, `Job.cs` and `JobConfiguration.cs` were later modified to include `Description` (text) and `LastSeenAt` (timestamp with time zone).
* **Impact:** The EF Core migration snapshot is out of sync with the domain model and configuration. Executing queries or inserts against PostgreSQL will immediately throw column not found exceptions.
* **Remedy:** Generate an EF Core migration (`AddJobDescriptionAndLastSeenAt` or comprehensive `FinalizeJobDomainModel`) once the Job entity is finalized.

### 2. Runtime EF Core `FindAsync` Key Resolution Bug
* **Problem:** In both `CompanySourceRepository.cs` (line 24) and `JobRepository.cs` (line 21):
  ```csharp
  return await _dbContext.Jobs.FindAsync(id, cancellationToken);
  ```
  `DbSet<T>.FindAsync` accepts `params object?[]? keyValues`. In C#, passing `(id, cancellationToken)` resolves to a composite key array `new object[] { id, cancellationToken }`.
* **Impact:** At runtime, EF Core throws an `ArgumentException` stating that 2 key values were supplied for a single-key entity.
* **Remedy:** Standardize across all repositories to use `FirstOrDefaultAsync(x => x.Id == id, cancellationToken)` or `FindAsync([id], cancellationToken)`.

### 3. Leaky API Contracts & Query Parameter Ingestion
* **Problem:** In `JobsController.cs`, `CompaniesController.cs`, and `CompanySourcesController`, action methods bind primitive values directly without request DTOs or `[FromBody]`:
  ```csharp
  [HttpPost]
  public async Task<IActionResult> CreateJob(
      string title, int companySourceId, string sourceJobId, Uri jobUrl,
      string? location = null, string? description = null, DateTimeOffset? datePosted = null,
      CancellationToken cancellationToken = default)
  ```
  In ASP.NET Core `[ApiController]`, primitive types default to `[FromQuery]`.
* **Impact:** Sending large job descriptions via query parameters in URLs violates HTTP conventions and risks 414 URI Too Long errors. Furthermore, returning domain entities directly exposes internal implementation details.
* **Remedy:** Introduce explicit request and response DTOs (`CreateJobRequest`, `JobResponse`, etc.).

### 4. Controller Filename Typo
* **Problem:** `src/Jobtri.Api/Controllers/CompanySourcesController .cs` contains an unwanted trailing space before `.cs`.
* **Remedy:** Rename the file to `CompanySourcesController.cs`.

### 5. Architectural Inversion in `IAtsConnector`
* **Problem:** `IAtsConnector.FetchJobsAsync` currently returns `Task<List<Job>>` (domain entity).
* **Impact:** ATS connectors are external data sources that should not know about domain invariants, canonical deduplication keys, or normalization logic.
* **Remedy:** Connectors should return `ExternalJob` DTOs:
  $$\text{External ATS} \longrightarrow \text{IAtsConnector} \longrightarrow \text{ExternalJob DTO} \longrightarrow \text{Normalization} \longrightarrow \text{Domain Job}$$

### 6. Documentation Divergence
* **Problem:** `PROJECT_DECISIONS.md` and `ARCHITECTURE.md` still mention SQLite as the MVP database, conflicting with `Jobtri-Current-State.md` and the actual PostgreSQL Docker configuration.
* **Remedy:** Update documentation to reflect PostgreSQL as the single source of truth from day one.

---

## 4. Target Job Domain Model Specification

To support deduplication, filtering, scoring, and tracking, the `Job` entity will encompass:

```text
Job
├── Id (int, Primary Key)
├── CanonicalJobId (string, Indexed — for cross-source deduplication)
├── CompanySourceId (int, Foreign Key)
├── SourceJobId (string, ATS-specific Job ID)
├── Title (string)
├── NormalizedTitle (string?, standardized role taxonomy)
├── JobUrl (Uri, posting URL)
├── ApplyUrl (Uri?, direct application link)
├── Location (string?)
├── City (string?)
├── Region (string?)
├── Country (string?)
├── RemoteType (enRemoteType)
├── EmploymentType (enEmploymentType)
├── Seniority (enSeniorityLevel)
├── ExperienceMin (int?)
├── ExperienceMax (int?)
├── Description (string?, full text)
├── Status (enJobStatus: Discovered, Normalized, Qualified, Disqualified, Applied, etc.)
├── DatePosted (DateTimeOffset?)
├── FirstSeenAt (DateTimeOffset)
└── LastSeenAt (DateTimeOffset)
```

### Supporting Enums
* `enJobStatus`: `Discovered = 1`, `Qualified = 2`, `Disqualified = 3`, `Shortlisted = 4`, `Applied = 5`, `Archived = 6`
* `enRemoteType`: `Unknown = 0`, `OnSite = 1`, `Hybrid = 2`, `Remote = 3`
* `enEmploymentType`: `Unknown = 0`, `FullTime = 1`, `PartTime = 2`, `Contract = 3`, `Internship = 4`, `Apprenticeship = 5`
* `enSeniorityLevel`: `Unknown = 0`, `EntryLevel = 1`, `Junior = 2`, `MidLevel = 3`, `Senior = 4`, `Lead = 5`

---

## 5. Execution Roadmap

### Step 1: Foundation Clean-Up & Bug Fixes
- [ ] Rename `CompanySourcesController .cs` to `CompanySourcesController.cs`.
- [ ] Fix `FindAsync` in `CompanySourceRepository` and `JobRepository`.
- [ ] Create API request and response DTOs for existing endpoints.

### Step 2: Finalize Job Domain Model & Enums
- [ ] Add domain enums (`enJobStatus`, `enRemoteType`, `enEmploymentType`, `enSeniorityLevel`).
- [ ] Enrich `Job` domain entity with business methods and invariants.
- [ ] Update and expand unit tests in `Jobtri.UnitTests`.

### Step 3: Persistence & EF Core Migration
- [ ] Update `JobConfiguration` with indexes and property constraints.
- [ ] Create and verify EF Core PostgreSQL migration.

### Step 4: ATS Connector Pipeline & First Integration
- [ ] Add `ExternalJob` record in `Jobtri.Application.Models`.
- [ ] Refactor `IAtsConnector` to return `IReadOnlyCollection<ExternalJob>`.
- [ ] Implement `GreenhouseConnector` with typed `HttpClient` registered via `HttpClientFactory`.
