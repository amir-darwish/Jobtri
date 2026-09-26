# Jobtri Current State

## Authority and classification

This status is based on the current repository code and configuration. Historical documents are reference material only.

## Implemented

### Solution and projects

- `Jobtri.slnx` includes four source projects and two test projects.
- All projects target `net10.0`.
- Nullable reference types and implicit usings are enabled.

### Domain

- `Company` validates and stores a name, optional website, and enabled state.
- `CompanySource` stores a company ID, careers URL, ATS enum, optional ATS identifier, and enabled state.
- `Job` stores source identity, title, URL, optional location and description, posting date, and first/last-seen timestamps.
- Basic URL, identifier, and state invariants are implemented.

### Application

- Company, company-source, and job services are registered through dependency injection.
- Persistence interfaces exist for the three current aggregates.
- `IAtsConnector` exists as an integration contract.

### Infrastructure and persistence

- `JobtriDbContext` exposes `Companies`, `CompanySources`, and `Jobs`.
- Fluent configurations define table names, constraints, conversions, indexes, and restricted foreign keys.
- PostgreSQL is configured with Npgsql in `Program.cs`.
- One initial EF Core migration exists.
- Repositories implement basic add, read, and save operations.

### API

Current routes are:

| Controller | Routes | Current behavior |
|---|---|---|
| Companies | `POST /api/Companies`, `GET /api/Companies`, `GET /api/Companies/{id}` | Create and read companies |
| Companies | `PUT /api/Companies/{id}/enable`, `PUT /api/Companies/{id}/disable` | Change enabled state |
| Companies | `PUT /api/Companies/{id}/website` | Update website |
| CompanySources | `POST /api/CompanySources`, `GET /api/CompanySources`, `GET /api/CompanySources/{id}` | Create and read sources |
| CompanySources | `PUT /api/CompanySources/{id}/enable`, `PUT /api/CompanySources/{id}/disable` | Change enabled state |
| Jobs | `POST /api/Jobs`, `GET /api/Jobs`, `GET /api/Jobs/{id}` | Create and read jobs |

Swagger/OpenAPI is enabled for Development. Authentication and authorization are absent.

## Partially implemented

- ATS integration has an interface but no connector implementations.
- PostgreSQL persistence exists, but the initial migration is inconsistent with the current `Job` model: `Description` and `LastSeenAt` are configured in code but absent from the migration.
- Unit tests cover domain construction and validation. The integration-test project exists but has no test source files.
- API actions use primitive parameters and return domain entities or anonymous IDs; request/response DTOs are not present.

## Missing

This list records absent capabilities, not a list of MVP prerequisites. The adopted initial scope serves the project owner or another GitHub user with manual criteria and filtering before main persistence. CV parsing/matching, AI analysis, auto-apply, SaaS architecture, multi-tenancy, complex user management, and advanced learning are not prerequisites. Mass auto-apply is prohibited.

- Concrete source integrations: direct ATS / official company ingestion and job-board ingestion.
- Company discovery and ATS detection workflows.
- Job normalization and cross-source deduplication.
- Manual criteria model and filtering service.
- Deterministic scoring and explanations.
- Application tracking, CV versions, analytics, and learning features.
- AI analysis or CV tailoring.
- Background processing, resilience policies, and health checks.
- Authentication and authorization.
- Production deployment and CI/CD configuration.

## Adopted pipeline gap — not implemented

[JobService.CreateAsync](../src/Jobtri.Application/Jobs/JobService.cs) constructs a job and immediately calls repository `AddAsync` and `SaveChangesAsync`. No manual-criteria filtering occurs in that flow.

The approved MVP requires acquisition, minimal normalization needed for filtering, deduplication where applicable, and hard/manual filters before relevant jobs enter the main Jobs store. Initial hard filters are country, language, contract type, domain / target role, and required experience. The existing unique source-job index is a persistence constraint, not an implemented ingestion/deduplication pipeline.

The decision is Adopted; implementation is pending. There is no temporary/staging workflow in the inspected source tree. Any future staging must remain distinct from main normalized Jobs persistence. Official ATS/company sources are preferred; job boards are also an approved source category, not an implemented integration.

## Known limitations and technical debt

- The migration/model mismatch may prevent reliable database operations until a migration is added or corrected.
- Historical API examples describe endpoints that do not exist in the current controllers.
- Direct primitive binding makes request contracts implicit and places large text fields in query-style parameters.
- Domain entities are returned directly from the API.
- The repository implementation uses simple repository methods without pagination, filtering, or explicit error contracts.
- The file `CompanySourcesController .cs` contains a trailing space in its filename; this is recorded as technical debt and is not changed by this documentation task.

## Needs confirmation

- The final ATS provider order.
- The final request/response DTO design.
- Whether the current migration should be replaced or extended after the `Job` model is finalized.
