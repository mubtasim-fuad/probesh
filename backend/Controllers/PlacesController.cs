using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Probesh.Api.Data;
using Probesh.Api.DTOs;
using Probesh.Api.Models;
using Probesh.Api.Services;

namespace Probesh.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PlacesController(
    ProbeshDbContext db,
    AccessibilityScoreCalculator scoreCalculator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<PlaceResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<PlaceResponse>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] PlaceCategory? category,
        CancellationToken cancellationToken)
    {
        var query = db.Places
            .AsNoTracking()
            .Include(place => place.Audits)
            .Where(place => place.VerificationStatus != PlaceVerificationStatus.Rejected);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(place =>
                place.Name.Contains(term) ||
                place.Area.Contains(term) ||
                place.City.Contains(term));
        }

        if (category.HasValue)
        {
            query = query.Where(place => place.Category == category.Value);
        }

        var places = await query
            .OrderByDescending(place => place.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return Ok(places.Select(ToResponse).ToArray());
    }

    [HttpGet("summary")]
    [ProducesResponseType<PlaceSummaryResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PlaceSummaryResponse>> GetSummary(CancellationToken cancellationToken)
    {
        var places = await db.Places
            .AsNoTracking()
            .Include(place => place.Audits)
            .Where(place => place.VerificationStatus != PlaceVerificationStatus.Rejected)
            .ToListAsync(cancellationToken);

        var latestAudits = places
            .Select(place => place.Audits.MaxBy(audit => audit.CreatedAtUtc))
            .Where(audit => audit is not null)
            .Cast<AccessibilityAudit>()
            .ToArray();

        var summary = new PlaceSummaryResponse(
            places.Count,
            places.Count(place => place.VerificationStatus == PlaceVerificationStatus.Verified),
            places.Count(place => place.VerificationStatus == PlaceVerificationStatus.PendingReview),
            latestAudits.Count(audit =>
                scoreCalculator.GetLevel(audit.Score, audit.KnownFeatureCount) ==
                AccessibilityLevel.HighlyAccessible),
            latestAudits.Length == 0
                ? 0
                : (int)Math.Round(latestAudits.Average(audit => audit.Score)));

        return Ok(summary);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<PlaceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlaceResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var place = await db.Places
            .AsNoTracking()
            .Include(item => item.Audits)
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

        return place is null ? NotFound() : Ok(ToResponse(place));
    }

    [HttpPost]
    [ProducesResponseType<PlaceResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlaceResponse>> Create(
        CreatePlaceRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Latitude == 0 && request.Longitude == 0)
        {
            ModelState.AddModelError(nameof(request.Latitude), "Please provide a real map location.");
            return ValidationProblem(ModelState);
        }

        var result = scoreCalculator.Calculate(request.Audit);
        var place = new Place
        {
            Name = request.Name.Trim(),
            Category = request.Category,
            Address = request.Address.Trim(),
            Area = request.Area.Trim(),
            City = request.City.Trim(),
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = CleanOptional(request.Description),
            VerificationStatus = PlaceVerificationStatus.PendingReview,
            CreatedAtUtc = DateTime.UtcNow
        };

        place.Audits.Add(new AccessibilityAudit
        {
            Place = place,
            StepFreeEntrance = request.Audit.StepFreeEntrance,
            AccessibleToilet = request.Audit.AccessibleToilet,
            Elevator = request.Audit.Elevator,
            WideDoorways = request.Audit.WideDoorways,
            AccessibleParking = request.Audit.AccessibleParking,
            StaffAssistance = request.Audit.StaffAssistance,
            Score = result.Score,
            KnownFeatureCount = result.KnownFeatureCount,
            Notes = CleanOptional(request.Audit.Notes),
            EvidencePhotoUrl = CleanOptional(request.Audit.EvidencePhotoUrl),
            CreatedAtUtc = DateTime.UtcNow
        });

        db.Places.Add(place);
        await db.SaveChangesAsync(cancellationToken);

        var response = ToResponse(place);
        return CreatedAtAction(nameof(GetById), new { id = place.Id }, response);
    }

    // Learning-MVP endpoint. Add reviewer authorization before production use.
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdatePlaceStatusRequest request,
        CancellationToken cancellationToken)
    {
        var place = await db.Places.SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (place is null)
        {
            return NotFound();
        }

        place.VerificationStatus = request.Status;
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    private PlaceResponse ToResponse(Place place)
    {
        var audit = place.Audits.MaxBy(item => item.CreatedAtUtc)
            ?? throw new InvalidOperationException("A place must have at least one accessibility audit.");

        var auditResponse = new AccessibilityAuditResponse(
            audit.StepFreeEntrance,
            audit.AccessibleToilet,
            audit.Elevator,
            audit.WideDoorways,
            audit.AccessibleParking,
            audit.StaffAssistance,
            audit.Score,
            audit.KnownFeatureCount,
            scoreCalculator.GetLevel(audit.Score, audit.KnownFeatureCount),
            audit.Notes,
            audit.EvidencePhotoUrl,
            audit.CreatedAtUtc);

        return new PlaceResponse(
            place.Id,
            place.Name,
            place.Category,
            place.Address,
            place.Area,
            place.City,
            place.Latitude,
            place.Longitude,
            place.Description,
            place.VerificationStatus,
            place.CreatedAtUtc,
            auditResponse);
    }

    private static string? CleanOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
