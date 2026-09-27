# Jobtri Roadmap

This roadmap distinguishes verified implementation from planned work. It does not imply that a phase is complete merely because it appeared in an older document.

## Adopted MVP scope

Jobtri is initially a Personal AI Job Assistant for one person validating the job-search workflow. Start with a manually defined `JobTarget` / search intent, then help the user find, filter, save, track, and act on opportunities. CV parsing and CV-based matching are later enhancements after the personal workflow and acquisition/filtering are validated.

Direct ATS / official company sources and job boards are acquisition options that support the workflow. Official ATS/company sources remain preferred when available; generic scraping is not the primary acquisition strategy. This does not make Jobtri a simple job aggregator or require delivering every source category in the first slice.

The MVP has no authentication, multi-user system, roles, subscriptions, or SaaS infrastructure. CV matching, AI analysis, advanced learning, and SaaS evolution are later phases. Human review before applying and no mass auto-apply remain constraints.

## Completed foundation

- Four-project .NET 10 solution.
- Domain, Application, Infrastructure, and API boundaries.
- PostgreSQL and EF Core setup.
- Initial Company, CompanySource, and Job entities.
- Basic application services, repositories, and controllers.
- Initial domain unit tests.

These components exist in code; this does not mean the acquisition/filtering workflow is complete or that database runtime behavior has been verified.

## Near-term priorities

### 1. Stabilize persistence infrastructure

This prepares reliable storage; it does not authorize storing unfiltered acquired jobs.

- Resolve the migration mismatch for `Job.Description` and `Job.LastSeenAt`.
- Verify repository key lookup behavior.
- Add meaningful integration tests against PostgreSQL.

### 2. Stabilize API contracts

- Decide whether to introduce explicit request and response DTOs.
- Define validation and error-response behavior.
- Add API result pagination/query filtering when needed. This is separate from the mandatory acquisition hard/manual filters before main persistence.

### 3. Define manual criteria and hard filters

- Accept manually entered criteria for country, language, contract type, domain / target role, and required experience.
- Define minimal normalization needed to evaluate those criteria.
- Confirm handling of missing or ambiguous filter data before claiming full coverage.
- Make filtering independent of CV parsing and CV-based matching.

### 4. Deliver the first personal workflow vertical slice

- Define the first `JobTarget` / search-intent input and the personal workflow around relevant opportunities.
- Support finding and filtering opportunities through the adopted acquisition boundary, including manually supplied source data while connectors remain planned.
- Save relevant opportunities and establish the planned application-tracking flow.
- Follow the adopted flow:

```text
Acquire jobs
→ minimal normalization required for filtering
→ deduplication where applicable
→ hard/manual filters
→ persist relevant jobs to the main job store
```

- Persist and retrieve only relevant jobs in the main normalized Jobs store.
- Verify with representative matching and non-matching records that the filtering gate precedes main persistence; verify applicable duplicate handling.
- Keep any future temporary raw processing/staging distinct from the main Jobs store. Staging is not a mandatory MVP component.

**Ordering correction (2026-09-26):** [D017](JOBTRI_DECISIONS.md#d017--filter-before-main-persistence) supersedes this roadmap's earlier persistence-before-filtering sequence. Manual criteria and the filtering gate belong in this first workflow, not a later phase. Current direct-save behavior still needs implementation work. Connector implementations follow the personal workflow foundation.

## Medium-term planned features

### 1. Personal workflow entities

- Define the planned `JobTarget` / search-intent concept.
- Add the planned `SavedJob` concept for opportunities the user wants to keep.
- Add application lifecycle tracking and the user workflow around saved opportunities.
- Keep these concepts single-user and independent of authentication or SaaS infrastructure.

### 2. Acquisition and processing expansion

- Implement direct ATS / official company connectors behind `IAtsConnector`.
- Add job-board integrations where they support the personal workflow.
- Company careers-page discovery and ATS detection.
- Broader normalization beyond the minimum needed for the initial filters.
- More advanced cross-source deduplication beyond the applicable MVP checks.
- Explainable deterministic scoring.
- Background discovery with cancellation and failure isolation.
- Structured logging, resilience, and health checks.
- CV-version association when application tracking requires it.

## Longer-term planned features

- AI-assisted job analysis after the personal workflow, manual filtering, and deterministic scoring are useful.
- CV parsing and CV-based matching after the basic acquisition/filtering workflow has been validated.
- CV and cover-letter tailoring with no invented qualifications.
- Analytics for response, interview, and offer outcomes.
- Outcome-driven learning improvements.
- Deployment automation and CI/CD.
- Possible multi-user SaaS evolution only if the personal workflow proves useful and a later decision approves its scope.

## Explicit constraints

- PostgreSQL remains the database provider unless a new decision record changes it.
- Human review remains required before applying.
- No mass auto-apply.
- No unnecessary MediatR, CQRS, generic repositories, or microservices.
- Historical roadmap claims are not current implementation evidence.

## Needs confirmation

- First ATS provider and delivery order.
- Final API DTO contract.
- Exact presentation of the first useful MVP search output; manual criteria and filtering before main persistence are already adopted.
- Missing-value handling for filters and any future staging design.
