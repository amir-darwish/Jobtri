# Jobtri Documentation Rules

## Purpose

These rules keep Jobtri documentation aligned with the repository and prevent historical plans from being mistaken for implemented behavior.

## Source-of-truth order

1. Current repository code and configuration
2. Latest approved architectural decisions
3. Existing documentation as historical reference only

When sources disagree, document the conflict and follow the higher-priority source for implementation facts. Code establishes current behavior; approved decisions establish intended behavior. Incomplete implementation does not silently supersede an approved decision.

## Classification rules

Every significant statement must be distinguishable as one of:

- **Current implementation**: verified in code, configuration, or an existing migration.
- **Adopted decision**: approved direction or constraint.
- **Planned feature**: future work that is not implemented.
- **Needs confirmation**: unresolved or unverifiable information.

Never label a roadmap item as implemented without a corresponding repository implementation.

## Maintenance workflow

### End-of-session documentation review

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

### Applying the review

1. Review all six Jobtri documents for relevant drift and cross-document contradictions.
2. Update affected sections only when the meaningful-change criteria above apply and the edit is authorized.
3. Verify implementation claims against saved code and configuration; distinguish approval from implementation.
4. Preserve decision IDs and valid decisions. For replacements, retain the prior decision's context and record a supersession note with its replacement and approval context.
5. Mark uncertain details as `Needs confirmation` instead of guessing.
6. Keep historical documents in `/docs/archive/` unchanged unless a separate task explicitly authorizes changes.

## What requires a decision record

Create or update a decision record for changes to:

- Database provider or persistence strategy.
- Project boundaries or dependency direction.
- API contract strategy.
- ATS/source acquisition priority, including permitted source categories.
- Acquisition filtering and the boundary between temporary/staging data and main Jobs persistence.
- MVP audience, matching strategy, prerequisites, and approved coding conventions.
- Human-review or auto-apply behavior.
- Introduction of MediatR, CQRS, generic repositories, microservices, or other significant infrastructure.
- AI usage, data handling, or CV integrity constraints.
- Authentication, authorization, or deployment architecture.

Routine bug fixes and small implementation details do not require an architectural decision record unless they change one of these constraints.

## Guidance for AI assistants

AI assistants must inspect the current repository before describing implementation status. They must treat historical documents as context, preserve adopted constraints, separate current behavior from plans, and identify uncertainty. They must not invent endpoints, entities, integrations, migrations, or services from proposals.

AI assistants must not make architectural, persistence, security, or workflow changes without explicit approval when the task scope requires approval.

## Rules preventing documentation drift

- Use exact project and file names from the repository.
- Link implementation claims to current code locations where practical.
- Keep route lists synchronized with controller attributes.
- Keep database descriptions synchronized with entity configurations and migrations.
- Distinguish configuration that exists from services that are merely planned.
- Review stale SQLite, endpoint, and feature claims whenever documentation is updated.
- Do not delete historical documents to hide contradictions; explain and archive them.
