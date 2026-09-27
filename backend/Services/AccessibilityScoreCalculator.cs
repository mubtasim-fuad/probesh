using Probesh.Api.DTOs;
using Probesh.Api.Models;

namespace Probesh.Api.Services;

public sealed record AccessibilityScoreResult(
    int Score,
    int KnownFeatureCount,
    AccessibilityLevel Level);

public sealed class AccessibilityScoreCalculator
{
    public AccessibilityScoreResult Calculate(AccessibilityAuditRequest request)
    {
        bool?[] answers =
        [
            request.StepFreeEntrance,
            request.AccessibleToilet,
            request.Elevator,
            request.WideDoorways,
            request.AccessibleParking,
            request.StaffAssistance
        ];

        var knownAnswers = answers.Where(answer => answer.HasValue).ToArray();
        if (knownAnswers.Length == 0)
        {
            return new AccessibilityScoreResult(0, 0, AccessibilityLevel.Unknown);
        }

        var yesAnswers = knownAnswers.Count(answer => answer is true);
        var score = (int)Math.Round(
            yesAnswers * 100d / knownAnswers.Length,
            MidpointRounding.AwayFromZero);

        var level = score switch
        {
            >= 80 => AccessibilityLevel.HighlyAccessible,
            >= 50 => AccessibilityLevel.PartiallyAccessible,
            _ => AccessibilityLevel.Limited
        };

        return new AccessibilityScoreResult(score, knownAnswers.Length, level);
    }

    public AccessibilityLevel GetLevel(int score, int knownFeatureCount)
    {
        if (knownFeatureCount == 0)
        {
            return AccessibilityLevel.Unknown;
        }

        return score switch
        {
            >= 80 => AccessibilityLevel.HighlyAccessible,
            >= 50 => AccessibilityLevel.PartiallyAccessible,
            _ => AccessibilityLevel.Limited
        };
    }
}
