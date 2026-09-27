namespace Probesh.Api.Models;

public sealed class Place
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public PlaceCategory Category { get; set; }
    public required string Address { get; set; }
    public required string Area { get; set; }
    public required string City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
    public PlaceVerificationStatus VerificationStatus { get; set; } = PlaceVerificationStatus.PendingReview;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public ICollection<AccessibilityAudit> Audits { get; set; } = new List<AccessibilityAudit>();
}
