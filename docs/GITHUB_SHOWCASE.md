# GitHub showcase guide

## Repository setup

The starter can be initialized locally with:

```bash
git init
git add .
git commit -m "feat: create Probesh accessibility MVP"
```

Create an empty public repository named `probesh-accessibility-map` on GitHub. Do not add another README or license there. Then run the two commands GitHub displays for adding the remote and pushing `main`.

## Recommended repository description

> Community accessibility map for Bangladesh built with Angular, ASP.NET Core, EF Core, and SQL.

## Suggested topics

`angular` · `aspnet-core` · `typescript` · `csharp` · `ef-core` · `sqlite` · `accessibility` · `social-impact`

## Before making it public

- Run the app from a clean clone.
- Run backend tests.
- Replace screenshots with your own working UI.
- Add a short demo GIF or video.
- Confirm there are no secrets, database files, or personal data in Git.
- Open three honest GitHub issues for planned features instead of pretending the MVP is complete.

## Strong first issues

1. `feat: add verified-only place filter`
2. `feat: store contributor information source`
3. `test: add create-place API integration tests`

## Commit style

Use small, meaningful commits:

```text
feat: add place submission form
fix: preserve unknown accessibility answers
test: cover accessibility score boundaries
docs: explain local setup
```

## Profile pin text

> **Probesh** — An accessibility-first community platform that helps people evaluate public places in Bangladesh. Built as a full-stack Angular + ASP.NET Core project with explainable scoring, structured audits, and a responsible path toward computer-vision assistance.

## 60-second demo structure

1. State the user problem in one sentence.
2. Search and filter the seeded places.
3. Open the submission form and show validation.
4. Submit a place and refresh to prove persistence.
5. Show the API/controller and scoring test briefly.
6. End with the next milestone, not a long feature list.
