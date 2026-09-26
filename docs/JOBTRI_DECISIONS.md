# Jobtri Decisions

## Decision policy

Decision status values are:

- **Adopted**: approved and reflected in the current direction or code.
- **Planned**: accepted future direction without current implementation.
- **Rejected**: explicitly not selected.
- **Needs confirmation**: insufficient evidence or unresolved conflict.

The source-of-truth order is current repository code and configuration, latest approved decisions, then historical documentation. Code determines implementation status; approved decisions determine intended behavior. An Adopted decision can await implementation. Record that gap instead of treating incomplete code as a reversal of the decision.

Decision IDs below belong to this consolidated register and remain stable; IDs in archived documents belong to their historical register. Clarifications restored on 2026-09-26 come from the project owner's explicit approval of the MVP decisions.

## Adopted decisions

### D001 — Project identity

**Status:** Adopted  
**Decision:** The project is named Jobtri.

### D002 — Primary platform

**Status:** Adopted  
**Decision:** Use C#, .NET 10, and ASP.NET Core.

### D003 — Four-project clean architecture

**Status:** Adopted  
**Decision:** Keep Domain, Application, Infrastructure, and Api as separate projects with dependency direction toward the domain.

### D004 — PostgreSQL from the beginning

**Status:** Adopted  
**Decision:** PostgreSQL is the database provider from the beginning, configured through EF Core and Npgsql. Historical SQLite references are obsolete.

### D005 — Avoid unnecessary complexity

**Status:** Adopted  
**Decision:** Do not introduce MediatR, CQRS, generic repositories, or microservices unless a concrete requirement justifies them.

### D006 — Official-source and ATS priority

**Status:** Adopted  
**Decision:** Jobtri may acquire jobs from both direct ATS / official company sources and job boards. Official ATS/company sources remain preferred when available. Generic scraping is not the primary acquisition strategy.

**Clarification (2026-09-26):** Job boards are an approved source category; this expands the previously incomplete description without removing official-source priority. Approval of a source category does not imply that its connector is implemented.

### D007 — Human review before applying

**Status:** Adopted  
**Decision:** Job recommendations remain subject to human review before an application is made.

### D008 — No mass auto-apply

**Status:** Adopted  
**Decision:** Jobtri must not automatically submit mass applications.

### D009 — Manual criteria first

**Status:** Adopted  
**Decision:** The MVP starts from manually entered job-search and filtering criteria. CV parsing and CV-based matching are not dependencies of the initial MVP. They are later enhancements after the basic acquisition/filtering workflow has been validated. Automated scoring and AI analysis are not prerequisites for that workflow.

**Clarification (2026-09-26):** This preserves manual criteria first and makes the absence of a CV dependency explicit.

### D017 — Filter before main persistence

**Status:** Adopted  
**Decision:** Acquired jobs must not be blindly stored in the main Jobs database. The intended MVP pipeline is:

```text
Acquire jobs
→ minimal normalization required for filtering
→ deduplication where applicable
→ hard/manual filters
→ persist relevant jobs to the main job store
```

Initial hard filters are country, language, contract type, domain / target role, and required experience.

Persist jobs relevant to the manually entered criteria in the main normalized Jobs store. If temporary raw processing or staging is needed later, distinguish it from main Jobs persistence; a staging design or store is not currently required or implemented.

**Implementation status:** The filtering gate is not implemented. `JobService.CreateAsync` currently constructs a job and saves it directly.

**Supersession (2026-09-26):** This approved decision supersedes the former consolidated roadmap ordering that persisted acquired ATS jobs before introducing manual/hard filtering. Existing persistence code is recorded as current behavior, not approval to bypass the intended gate.

### D018 — MVP user and product scope

**Status:** Adopted  
**Decision:** The initial user is the project owner or another developer/user running Jobtri from GitHub. The MVP does not require SaaS architecture, multi-tenancy, or complex user management. Jobtri may evolve into SaaS later if the workflow proves useful; this is a possibility, not an MVP commitment.

### D019 — Approved coding conventions

**Status:** Adopted  
**Decision:** Use `int` IDs unless scale genuinely requires otherwise. Enum type names use the `en` prefix. Keep method and constructor parameter lists on one line when practical. Keep the architecture intentionally simple, consistent with D005.

These are approved conventions, not a claim that every existing signature is formatted this way. This documentation update does not authorize code formatting or refactoring.

## Planned decisions

### D010 — ATS connector implementations

**Status:** Planned  
**Decision:** Implement concrete connectors behind the existing `IAtsConnector` abstraction, starting with approved ATS providers.

### D011 — Normalization and deduplication

**Status:** Planned  
**Decision:** Implement normalization and deduplication for supported source data. The MVP needs minimal normalization required for filtering and deduplication where applicable before main persistence, as adopted in D017. Broader normalization and cross-source matching remain future enhancements.

**Status clarification:** The implementation work remains Planned; the pipeline ordering in D017 is Adopted.

### D012 — Deterministic scoring

**Status:** Planned  
**Decision:** Add explainable deterministic scoring after manual filtering and normalization are available.

### D013 — AI assistance after filtering

**Status:** Planned  
**Decision:** Use AI only for selected opportunities after hard/manual criteria and deterministic processing reduce the candidate set.

### D014 — Application tracking and analytics

**Status:** Planned  
**Decision:** Add application lifecycle tracking and outcome analytics after the core job pipeline is useful.

## Rejected decisions

No rejected decisions were explicitly recorded in the current repository documentation.

## Needs confirmation

### D015 — Exact ATS implementation order

**Status:** Needs confirmation  
The historical documents mention Greenhouse, Lever, SmartRecruiters, and SuccessFactors, but the repository does not implement any of them. Their final delivery order requires confirmation.

### D016 — API contract evolution

**Status:** Needs confirmation  
The current controllers bind primitive parameters and return domain entities. A future DTO-based contract is described historically, but its scope and timing require confirmation.
