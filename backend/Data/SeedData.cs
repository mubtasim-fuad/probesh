using Microsoft.EntityFrameworkCore;
using Probesh.Api.Models;

namespace Probesh.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(ProbeshDbContext db)
    {
        if (await db.Places.AnyAsync())
        {
            return;
        }

        var places = new[]
        {
            CreateDemoPlace(
                "Demo Community Clinic",
                PlaceCategory.Healthcare,
                "Sample address — not a real accessibility claim",
                "Mirpur",
                23.8223,
                90.3654,
                [true, false, null, true, true, true],
                "Demo data: call the venue before travelling."),
            CreateDemoPlace(
                "Demo Public Library",
                PlaceCategory.Education,
                "Sample address — not a real accessibility claim",
                "Dhanmondi",
                23.7465,
                90.3760,
                [true, true, true, true, false, true],
                "Demo data created only to show the interface."),
            CreateDemoPlace(
                "Demo Neighbourhood Café",
                PlaceCategory.Restaurant,
                "Sample address — not a real accessibility claim",
                "Uttara",
                23.8759,
                90.3795,
                [false, null, false, true, null, true],
                "Some answers are unknown, which is different from no.")
        };

        db.Places.AddRange(places);
        await db.SaveChangesAsync();
    }

    private static Place CreateDemoPlace(
        string name,
        PlaceCategory category,
        string address,
        string area,
        double latitude,
        double longitude,
        bool?[] answers,
        string notes)
    {
        var known = answers.Count(answer => answer.HasValue);
        var yes = answers.Count(answer => answer is true);
        var score = known == 0 ? 0 : (int)Math.Round(yes * 100d / known);

        var place = new Place
        {
            Name = name,
            Category = category,
            Address = address,
            Area = area,
            City = "Dhaka",
            Latitude = latitude,
            Longitude = longitude,
            Description = "A clearly labelled sample record for local development.",
            VerificationStatus = PlaceVerificationStatus.Verified,
            CreatedAtUtc = DateTime.UtcNow
        };

        place.Audits.Add(new AccessibilityAudit
        {
            Place = place,
            StepFreeEntrance = answers[0],
            AccessibleToilet = answers[1],
            Elevator = answers[2],
            WideDoorways = answers[3],
            AccessibleParking = answers[4],
            StaffAssistance = answers[5],
            Score = score,
            KnownFeatureCount = known,
            Notes = notes,
            CreatedAtUtc = DateTime.UtcNow
        });

        return place;
    }
}
