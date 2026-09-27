namespace Probesh.Api.Models;

public sealed class AccessibilityAudit
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlaceId { get; set; }
    public Place Place { get; set; } = null!;

    public bool? StepFreeEntrance { get; set; }
    public bool? AccessibleToilet { get; set; }
    public bool? Elevator { get; set; }
    public bool? WideDoorways { get; set; }
    public bool? AccessibleParking { get; set; }
    public bool? StaffAssistance { get; set; }

    public int Score { get; set; }
    public int KnownFeatureCount { get; set; }
    public string? Notes { get; set; }
    public string? EvidencePhotoUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
