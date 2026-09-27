# Learning journey

This project is your practical Angular + .NET course. Each checkpoint has a lesson, a coding task, and proof. Do not unlock the next checkpoint until you can explain the current one in your own words.

## Checkpoint 0 — Run the vertical slice

**Learn:** terminal navigation, frontend/backend processes, ports, and HTTP.

**Task:** run the API and Angular app, open the place list, submit one test place, and refresh the page.

**Pass criteria:**

- Both processes start without errors.
- The browser loads records from the API.
- Your submitted record remains after refresh.
- You can explain why both `localhost:4200` and `localhost:5074` are needed.

**Proof to share with your teacher:** terminal screenshots, browser screenshot, and your four-sentence explanation of the request flow.

## Checkpoint 1 — Understand one Angular component

**Learn:** component class, template, SCSS, signal, computed value, and lifecycle.

**Task:** add an “Only verified” filter to the place list.

**Pass criteria:** filtering happens without reloading the page, keyboard users can operate it, and the empty state still works.

## Checkpoint 2 — Master reactive forms

**Learn:** `FormGroup`, controls, validators, touched/invalid state, and payload mapping.

**Task:** add a required “source of information” field with the values `Visited personally`, `Called the venue`, and `Other`.

**Pass criteria:** Angular blocks an empty submission and the API independently rejects invalid data.

## Checkpoint 3 — Follow data through the full stack

**Learn:** DTO → controller → service → entity → EF Core → SQL.

**Task:** add a `ContactPhone` field from the Angular form through to the database and list response.

**Pass criteria:** the value survives a restart and no entity is returned directly from a controller.

## Checkpoint 4 — Complete CRUD

**Learn:** route parameters, PUT/PATCH/DELETE, optimistic UI choices, and confirmation states.

**Task:** build place detail, edit, and safe delete/archive flows.

**Pass criteria:** invalid IDs return 404, invalid changes return 400, and archived records do not appear publicly.

## Checkpoint 5 — Use real database migrations

**Learn:** migrations, schema history, configuration, and environment variables.

**Task:** replace `EnsureCreated`, create the first migration, and switch development to PostgreSQL.

**Pass criteria:** a clean database can be created from migrations and secrets are not committed.

## Checkpoint 6 — Add authentication and roles

**Learn:** identity, password hashing, tokens/cookies, authorization, and ownership.

**Task:** add Contributor and Reviewer roles. Only reviewers can verify or reject a submission.

**Pass criteria:** authorization is enforced by the API, not only hidden in Angular.

## Checkpoint 7 — Evidence photos and moderation

**Learn:** multipart upload, object storage, content-type/size checks, and abuse prevention.

**Task:** replace photo URLs with safe uploads and create a reviewer queue.

**Pass criteria:** invalid file types fail, private metadata is stripped, and rejected photos are removable.

## Checkpoint 8 — Map and location search

**Learn:** coordinates, bounding boxes, third-party APIs, and geospatial queries.

**Task:** add an OpenStreetMap view and “near me” search.

**Pass criteria:** the list remains usable without location permission and the map is keyboard-accessible where possible.

## Checkpoint 9 — Quality engineering

**Learn:** unit, integration, and end-to-end test boundaries; logging and error handling.

**Task:** test the create-place endpoint and the main Angular submission flow.

**Pass criteria:** tests run in one command and catch a deliberately introduced scoring bug.

## Checkpoint 10 — Deploy and showcase

**Learn:** CI, production configuration, deployment, monitoring, README writing, and demos.

**Task:** deploy Angular, API, and PostgreSQL; record a 60–90 second demo.

**Pass criteria:** a recruiter can open the live demo, understand the problem in 15 seconds, and find setup instructions and architecture in GitHub.

## Optional Checkpoint 11 — Responsible CV assistant

**Learn:** dataset quality, model evaluation, confidence, inference APIs, and human review.

**Task:** train a small model that suggests one photo attribute, such as “steps visible / no steps visible / uncertain.”

**Pass criteria:** suggestions are stored separately, confidence is shown, uncertain cases go to humans, and false positives are measured.
