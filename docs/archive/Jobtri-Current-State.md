# Jobtri - Current State

Last Updated: 2026-09-24

---

# 1. Project Overview

## Project Name

Jobtri

## Vision

Jobtri is not a simple job scraper or job board.

It is a personal Job Intelligence and Decision Support System designed to help job seekers discover, analyze, prioritize, and manage job opportunities efficiently.

The system follows a pipeline:

Company Discovery
        ↓
ATS Detection
        ↓
Job Acquisition
        ↓
Normalization
        ↓
Deduplication
        ↓
Database Storage
        ↓
Hard Filters
        ↓
Deterministic Scoring
        ↓
AI Analysis
        ↓
Human Review
        ↓
Apply
        ↓
Tracking
        ↓
Learning Engine


---

# 2. Core Philosophy

Jobtri should answer:

"Among thousands of available jobs, which ones are actually worth my time and why?"

The system should not blindly collect jobs.

The objective is:

- Reduce noise.
- Find relevant opportunities.
- Explain recommendations.
- Improve through feedback.


---

# 3. What Jobtri Is NOT

Jobtri is NOT:

- A simple web scraper.
- A LinkedIn scraper.
- An auto-apply bot.
- A generic job board.
- A CRUD application for storing jobs.


The focus is intelligence and decision support.


---

# 4. Comparison With Similar Projects

## JobSync

JobSync focuses mainly on:

- Job application tracking.
- Resume matching.
- AI assistance.
- Application analytics.

Workflow:

Job Found
    ↓
Save Job
    ↓
Analyze CV Match
    ↓
Apply
    ↓
Track


## Jobtri

Jobtri starts earlier:

Thousands of Jobs
    ↓
Acquire
    ↓
Normalize
    ↓
Remove duplicates
    ↓
Filter
    ↓
Score
    ↓
Analyze
    ↓
Recommend


JobSync ideas can be integrated later, especially:

- Application tracking.
- CV versions.
- Interview preparation.
- Feedback learning.


But they are not the current priority.


---

# 5. Current Architecture

Architecture style:

Clean Architecture


Solution:

Jobtri.sln


Projects:

src/

    Jobtri.Domain

    Jobtri.Application

    Jobtri.Infrastructure

    Jobtri.Api



Responsibilities:

## Domain

Contains:

- Entities
- Enums
- Business concepts

No dependency on other layers.


## Application

Contains:

- Business workflows.
- Interfaces.
- Services.

Example:

CompanyService

ATS abstractions


## Infrastructure

Contains:

- Database implementation.
- EF Core.
- External integrations.

Examples:

- PostgreSQL
- ATS connectors


## Api

Contains:

- REST endpoints.
- Controllers.
- Dependency Injection.


---

# 6. Technology Decisions

## Backend

ASP.NET Core

.NET 10


## Database

PostgreSQL from the beginning.

Reason:

Avoid migration from SQLite later.

Local development:

Docker Compose PostgreSQL


## ORM

Entity Framework Core

Provider:

Npgsql


## Secrets

Development:

ASP.NET Core User Secrets


Docker:

.env file

Never commit secrets.


---

# 7. Engineering Rules

These decisions are fixed unless intentionally changed.

## IDs

Use:

int

Not long.

Reason:

Expected scale does not require long.


## Enum Naming

Use:

en prefix

Example:

enAtsType


## Code Formatting

Keep constructor and method parameters in one line whenever practical.


## Architecture

Avoid unnecessary complexity.

Do NOT introduce:

- CQRS
- MediatR
- Generic Repository
- Excessive abstractions

unless a real need appears.


---

# 8. Current Domain Status


Completed entities:

## Company

Purpose:

Represents hiring organizations.


## CompanySource

Purpose:

Represents job sources.

Examples:

- Company career page.
- ATS endpoint.


## Job

Initial version created.

Current role:

Core entity representing acquired job opportunities.


Expected Job data:

- Id
- CompanySourceId
- SourceJobId
- Title
- JobUrl
- ApplyUrl
- Location
- Description
- DatePosted
- FirstSeenAt
- LastSeenAt


Future fields:

- EmploymentType
- Seniority
- Experience
- RemoteType
- NormalizedTitle
- Skills


---

# 9. Current ATS Integration Status

Current milestone:

Create ATS integration foundation.


Implemented:

Interface:

IAtsConnector


Concept:

Each ATS has its own connector.


Example:

IAtsConnector

Properties:

- ATS Type


Methods:

- FetchJobsAsync()


The connector responsibility:

Retrieve external jobs.

It should NOT:

- Score jobs.
- Analyze CV.
- Apply filters.


Its only job:

Acquire raw jobs.


---

# 10. ATS Concern

Important design decision:

Different ATS platforms have different structures.

Example:

Greenhouse:
- JSON API

Lever:
- JSON API

Ashby:
- Different schema


Solution:

Never store ATS raw structure directly.


Flow:

External ATS

      ↓

ATS Connector

      ↓

External Job DTO

      ↓

Normalization

      ↓

Domain Job Entity


---

# 11. Data Pipeline


## Acquisition

Collect jobs.


Sources priority:

1. Official ATS APIs

2. Public APIs

3. Structured Data / JSON-LD

4. HTTP Extraction

5. Scrapling

6. Browser Automation only if necessary


---

# 12. Database Strategy

The database stores:

Not every job forever.

It stores:

Useful normalized opportunities.


Future strategy:

Acquisition:
Store raw job data temporarily.

After processing:

Normalize

Deduplicate

Filter

Keep valuable jobs.


---

# 13. Upcoming Implementation Roadmap


## Phase 1

Finalize Job Entity

Create:

- Job configuration.
- EF migration.
- Database table.


## Phase 2

Job Persistence

Create:

- Repository.
- Service.
- API endpoints.


## Phase 3

First ATS Integration

Start with one ATS:

Recommended:

Greenhouse or Lever


Goal:

End-to-end flow:

ATS

↓

Connector

↓

Database


## Phase 4

Normalization

Implement:

- Title normalization.
- Location normalization.
- Skills extraction.


## Phase 5

Filtering

Hard rules:

Examples:

- Location.
- Experience.
- Remote policy.
- Language.


## Phase 6

Scoring Engine

Deterministic first.

Example:

Skills match:
40%

Experience:
25%

Location:
20%

Company:
15%


AI explanation comes after.


## Phase 7

AI Layer

Only analyze top-ranked jobs.

Avoid unnecessary LLM cost.


## Phase 8

Application Tracking

Inspired by JobSync:

Add:

- Applications.
- Status history.
- CV versions.
- Interview notes.


## Phase 9

Learning Engine

Learn from:

- Applications.
- Replies.
- Interviews.
- Offers.


---

# 14. Current Development Position

Completed:

[x] Solution structure

[x] Clean Architecture

[x] PostgreSQL setup

[x] EF Core setup

[x] Initial migrations

[x] Company entity

[x] CompanySource entity

[x] Company CRUD

[x] CompanySource CRUD

[x] ATS connector abstraction


Current:

Finalize Job domain model.


Next:

1. Job entity completion.

2. EF configuration.

3. Migration.

4. Job persistence.

5. First ATS connector.


---

# 15. Important Reminder For Future Conversations

When continuing Jobtri:

Always consider this document the source of truth.

Do not redesign architecture without checking existing decisions.

Priorities:

1. Build working pipeline.

2. Keep architecture simple.

3. Avoid premature AI.

4. Avoid unnecessary scraping complexity.

5. Prefer official sources.


---

# End