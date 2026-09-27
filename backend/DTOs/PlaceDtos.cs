using System.ComponentModel.DataAnnotations;
using Probesh.Api.Models;

namespace Probesh.Api.DTOs;

public sealed class CreatePlaceRequest
{
    [Required, StringLength(120, MinimumLength = 2)]
    public required string Name { get; init; }

    [EnumDataType(typeof(PlaceCategory))]
    public PlaceCategory Category { get; init; }

    [Required, StringLength(240, MinimumLength = 5)]
    public required string Address { get; init; }

    [Required, StringLength(80, MinimumLength = 2)]
    public required string Area { get; init; }

    [Required, StringLength(80, MinimumLength = 2)]
    public required string City { get; init; }

    [Range(-90, 90)]
    public double Latitude { get; init; }

    [Range(-180, 180)]
    public double Longitude { get; init; }

    [StringLength(800)]
    public string? Description { get; init; }

    [Required]
    public required AccessibilityAuditRequest Audit { get; init; }
}

public sealed class AccessibilityAuditRequest
{
    public bool? StepFreeEntrance { get; init; }
    public bool? AccessibleToilet { get; init; }
    public bool? Elevator { get; init; }
    public bool? WideDoorways { get; init; }
    public bool? AccessibleParking { get; init; }
    public bool? StaffAssistance { get; init; }

    [StringLength(800)]
    public string? Notes { get; init; }

    [Url, StringLength(500)]
    public string? EvidencePhotoUrl { get; init; }
}

public sealed class UpdatePlaceStatusRequest
{
    [EnumDataType(typeof(PlaceVerificationStatus))]
    public PlaceVerificationStatus Status { get; init; }
}

public sealed record AccessibilityAuditResponse(
    bool? StepFreeEntrance,
    bool? AccessibleToilet,
    bool? Elevator,
    bool? WideDoorways,
    bool? AccessibleParking,
    bool? StaffAssistance,
    int Score,
    int KnownFeatureCount,
    AccessibilityLevel Level,
    string? Notes,
    string? EvidencePhotoUrl,
    DateTime CreatedAtUtc);

public sealed record PlaceResponse(
    Guid Id,
    string Name,
    PlaceCategory Category,
    string Address,
    string Area,
    string City,
    double Latitude,
    double Longitude,
    string? Description,
    PlaceVerificationStatus VerificationStatus,
    DateTime CreatedAtUtc,
    AccessibilityAuditResponse Audit);

public sealed record PlaceSummaryResponse(
    int TotalPlaces,
    int VerifiedPlaces,
    int PendingPlaces,
    int HighlyAccessiblePlaces,
    int AverageScore);
