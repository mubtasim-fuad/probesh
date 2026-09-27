# Database

The MVP uses SQLite through EF Core. On first API startup, `EnsureCreatedAsync` creates the local `probesh.db` from the entity model and seeds clearly labeled demo places. The database file is ignored by Git.

Main entities: `Place` has many `AccessibilityAudit` records. See `backend/Models/` and `backend/Data/ProbeshDbContext.cs` for the authoritative schema and relationships.

EF Core migrations have **not** been added yet. Moving from `EnsureCreated` to versioned migrations and PostgreSQL is a future milestone; do not use this starter's schema creation approach for a production database.
