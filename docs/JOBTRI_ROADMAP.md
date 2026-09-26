# Jobtri Roadmap

This roadmap distinguishes verified implementation from planned work. It does not imply that a phase is complete merely because it appeared in an older document.

## Adopted MVP scope

The initial user is the project owner or another developer/user running Jobtri from GitHub. Start with manually entered job-search and filtering criteria. CV parsing and CV-based matching are later enhancements after acquisition/filtering is validated.

Direct ATS / official company sources and job boards are permitted. Official ATS/company sources remain preferred when available; generic scraping is not the primary acquisition strategy. This does not require delivering every source category in the first slice.

CV matching, AI analysis, auto-apply, SaaS architecture, multi-tenancy, complex user management, and advanced learning are not MVP prerequisites. SaaS is a possible later evolution if the workflow proves useful. Human review before applying and no mass auto-apply remain constraints.

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

### 4. Deliver the first acquisition/filtering vertical slice

- Select the first official ATS/company source and implement its connector behind the Application contract.
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

**Ordering correction (2026-09-26):** [D017](JOBTRI_DECISIONS.md#d017--filter-before-main-persistence) supersedes this roadmap's earlier persistence-before-filtering sequence. Manual criteria and the filtering gate belong in this first workflow, not a later phase. Current direct-save behavior still needs implementation work.

## Medium-term planned features

- Company careers-page discovery and ATS detection.
- Broader normalization beyond the minimum needed for the initial filters.
- More advanced cross-source deduplication beyond the applicable MVP checks.
- Explainable deterministic scoring.
- Background discovery with cancellation and failure isolation.
- Structured logging, resilience, and health checks.
- Application lifecycle tracking and CV-version association.

## Longer-term planned features

- AI-assisted job analysis after manual filtering and deterministic scoring.
- CV parsing and CV-based matching after the basic acquisition/filtering workflow has been validated.
- CV and cover-letter tailoring with no invented qualifications.
- Analytics for response, interview, and offer outcomes.
- Outcome-driven learning improvements.
- Additional official ATS/company integrations and job-board integrations.
- Deployment automation and CI/CD.
- Possible SaaS evolution only if the workflow proves useful and a later decision approves its scope.

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
