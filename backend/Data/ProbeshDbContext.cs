using Microsoft.EntityFrameworkCore;
using Probesh.Api.Models;

namespace Probesh.Api.Data;

public sealed class ProbeshDbContext(DbContextOptions<ProbeshDbContext> options)
    : DbContext(options)
{
    public DbSet<Place> Places => Set<Place>();
    public DbSet<AccessibilityAudit> AccessibilityAudits => Set<AccessibilityAudit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>(entity =>
        {
            entity.Property(place => place.Name).HasMaxLength(120);
            entity.Property(place => place.Address).HasMaxLength(240);
            entity.Property(place => place.Area).HasMaxLength(80);
            entity.Property(place => place.City).HasMaxLength(80);
            entity.Property(place => place.Description).HasMaxLength(800);
            entity.Property(place => place.Category).HasConversion<string>();
            entity.Property(place => place.VerificationStatus).HasConversion<string>();
            entity.HasIndex(place => new { place.City, place.Area });
            entity.HasIndex(place => place.Category);
        });

        modelBuilder.Entity<AccessibilityAudit>(entity =>
        {
            entity.Property(audit => audit.Notes).HasMaxLength(800);
            entity.Property(audit => audit.EvidencePhotoUrl).HasMaxLength(500);
            entity
                .HasOne(audit => audit.Place)
                .WithMany(place => place.Audits)
                .HasForeignKey(audit => audit.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
