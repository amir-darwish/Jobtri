# Jobtri AI Context

## Project purpose

Jobtri is intended to help a job seeker discover, normalize, prioritize, and track relevant opportunities from direct ATS / official company sources and job boards. Official ATS/company sources are preferred when available; generic scraping is not the primary acquisition strategy. It is a decision-support system. A human reviews opportunities before applying, and the product must not become a mass auto-apply bot.

## Current implementation

The repository currently implements:

- A four-project .NET 10 solution.
- Domain entities for companies, company sources, and jobs.
- Application services and persistence contracts for those entities.
- EF Core persistence configured for PostgreSQL.
- Basic REST endpoints for companies, company sources, and jobs.
- An `IAtsConnector` abstraction without concrete ATS connectors.

The following are not implemented in the current repository: ATS fetching, normalization, deduplication, scoring, applications, analytics, AI analysis, background discovery, and authentication.

## Adopted MVP scope and pipeline

The initial user is the project owner or another developer/user running Jobtri from GitHub. The MVP does not require SaaS architecture, multi-tenancy, or complex user management. SaaS is a possible later evolution if the workflow proves useful.

Start from manually entered job-search and filtering criteria. CV parsing and CV-based matching are later enhancements after validating basic acquisition/filtering, not initial MVP dependencies. AI analysis, auto-apply, SaaS features, and advanced learning are not MVP prerequisites; mass auto-apply remains prohibited.

Follow the adopted acquisition pipeline:

```text
Acquire jobs
→ minimal normalization required for filtering
→ deduplication where applicable
→ hard/manual filters
→ persist relevant jobs to the main job store
```

Initial hard filters are country, language, contract type, domain / target role, and required experience.

Do not blindly persist acquired jobs in the main Jobs store. Future temporary raw processing/staging must be distinct from main normalized Jobs persistence. No staging implementation is implied.

**Current implementation gap:** `JobService.CreateAsync` saves directly without the hard/manual filtering gate. The pipeline above is Adopted, not Implemented. Code describes current behavior; approved decisions define the intended behavior.

## Approved coding conventions

- Use `int` IDs unless scale genuinely requires otherwise.
- Enum type names use the `en` prefix.
- Keep method and constructor parameter lists on one line when practical.
- Keep the architecture intentionally simple.

These conventions do not authorize unrelated changes to existing code.

## Architecture philosophy

- Keep Domain independent from infrastructure and transport concerns.
- Keep business orchestration in Application services.
- Keep EF Core, PostgreSQL, and external integrations in Infrastructure.
- Keep API controllers thin.
- Prefer small, feature-oriented services.
- Avoid MediatR, CQRS, generic repositories, and microservices unless a documented decision justifies them.
- Prefer official ATS and company sources over generic scraping.
- Use manually entered search criteria and hard filters before main Jobs persistence, independently of CV parsing or matching.
- Keep human review before any application action.

## Constraints for AI assistants

AI assistants must not:

- Claim roadmap features are implemented without verifying source code.
- Change the database provider away from PostgreSQL without an approved decision.
- Introduce new projects, architectural patterns, migrations, external integrations, or dependencies without approval.
- Add mass auto-apply behavior.
- Remove human review from the application workflow.
- Put EF Core, HTTP clients, or ATS-specific logic into Domain.
- Treat historical documentation as stronger evidence than the current repository.
- Fix unrelated technical debt during a narrowly scoped task.
- Modify migrations, source code, tests, Docker files, or environment configuration during documentation-only work.

## End-of-session documentation review

Before finishing a meaningful Codex development session, review the Jobtri documentation for drift.

Update documentation only when the session introduced a meaningful change to:

- Architecture.
- Approved decisions.
- MVP scope.
- Technology choices.
- Important constraints.
- Implementation status.
- Major completed features.

Do not update documentation for trivial edits, debugging, formatting, or temporary implementation details. Reviewing for drift does not require an edit when the documentation remains accurate, and it does not override the repository's authorization workflow.

Preserve previous valid decisions. Do not silently remove or rewrite historical decisions. If a decision is replaced, document that it was superseded, identify its replacement and approval context, and retain the historical record.

## Documentation classification

Every important statement should be classified as one of:

- **Current implementation**: verified in current code or configuration.
- **Adopted decision**: approved architectural or product constraint.
- **Planned feature**: future work from the roadmap.
- **Needs confirmation**: uncertain or not verifiable from the repository.
