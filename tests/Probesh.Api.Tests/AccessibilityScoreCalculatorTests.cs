using Probesh.Api.DTOs;
using Probesh.Api.Models;
using Probesh.Api.Services;
using Xunit;

namespace Probesh.Api.Tests;

public sealed class AccessibilityScoreCalculatorTests
{
    private readonly AccessibilityScoreCalculator _calculator = new();

    [Fact]
    public void Calculate_WhenAllAnswersUnknown_ReturnsUnknown()
    {
        var request = new AccessibilityAuditRequest();

        var result = _calculator.Calculate(request);

        Assert.Equal(0, result.Score);
        Assert.Equal(0, result.KnownFeatureCount);
        Assert.Equal(AccessibilityLevel.Unknown, result.Level);
    }

    [Fact]
    public void Calculate_ScoresOnlyKnownAnswers()
    {
        var request = new AccessibilityAuditRequest
        {
            StepFreeEntrance = true,
            AccessibleToilet = false,
            Elevator = null,
            WideDoorways = true,
            AccessibleParking = null,
            StaffAssistance = true
        };

        var result = _calculator.Calculate(request);

        Assert.Equal(75, result.Score);
        Assert.Equal(4, result.KnownFeatureCount);
        Assert.Equal(AccessibilityLevel.PartiallyAccessible, result.Level);
    }

    [Theory]
    [InlineData(49, AccessibilityLevel.Limited)]
    [InlineData(50, AccessibilityLevel.PartiallyAccessible)]
    [InlineData(79, AccessibilityLevel.PartiallyAccessible)]
    [InlineData(80, AccessibilityLevel.HighlyAccessible)]
    public void GetLevel_UsesDocumentedBoundaries(int score, AccessibilityLevel expected)
    {
        var level = _calculator.GetLevel(score, knownFeatureCount: 6);

        Assert.Equal(expected, level);
    }
}
