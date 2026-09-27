# Product brief

## One-sentence pitch

Probesh is a community-maintained map that helps people discover how accessible public places in Bangladesh really are before they travel.

## The problem

Most listings only show an address, opening time, and a generic accessibility label. A wheelchair user, an older adult, a parent with a stroller, or someone recovering from injury needs specific answers:

- Is there a step-free entrance?
- Are doorways wide enough?
- Is there a usable elevator?
- Is there an accessible toilet?
- Is accessible parking available?
- Can staff provide assistance?

Probesh records these answers separately and shows how the overall score was calculated.

## Primary users

1. **Visitor** — searches for a place and checks its accessibility details.
2. **Contributor** — adds or updates a place after visiting it.
3. **Reviewer** — verifies evidence and handles inaccurate submissions.
4. **Organization owner (later)** — claims a place and publishes improvements.

## MVP user story

> As a visitor, I want to search for a nearby place and see its accessibility checklist so I can decide whether it meets my needs.

> As a contributor, I want to submit a place and evidence so useful local information becomes available to others.

## MVP scope

### Included now

- Place list, search, and filters
- Place submission form
- Six-item accessibility checklist
- Explainable score
- Pending/verified/rejected review status in the data model
- API summary counts
- Responsive and keyboard-friendly interface

### Deliberately postponed

- Login and roles
- Reviewer dashboard
- Map view and distance search
- Direct photo upload
- Comments and notifications
- Organization accounts
- AI-assisted photo classification

## Success criteria

The learning MVP is complete when:

- A new place submitted in Angular is validated by the API and stored in SQL.
- Refreshing the browser keeps the record.
- Search and filters work with keyboard navigation.
- The score can be explained from the six checklist answers.
- At least one backend business rule has automated tests.
- A stranger can run the project by following the README.

## Product principles

- **Specific over vague:** show individual features, not only a green badge.
- **Evidence over confidence:** distinguish unknown, community-submitted, and verified data.
- **Accessible by default:** keyboard support, visible focus, semantic HTML, good contrast.
- **Human-reviewed AI:** future AI can assist; it cannot certify a place.
- **Small releases:** one working vertical slice before adding complexity.

## Portfolio elevator pitch

“I built Probesh, an Angular and ASP.NET Core accessibility platform for Bangladesh. Instead of using one vague accessibility label, it stores a structured audit and calculates an explainable score. I designed the first release as a modular monolith with an Angular frontend, validated REST API, EF Core, and SQL. I also treated accessibility and data trust as product requirements, not decorative features.”
