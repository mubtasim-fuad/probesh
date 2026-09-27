namespace Probesh.Api.Models;

public enum PlaceCategory
{
    Healthcare,
    Education,
    Government,
    Transport,
    Restaurant,
    Shopping,
    Recreation,
    Other
}

public enum PlaceVerificationStatus
{
    PendingReview,
    Verified,
    Rejected
}

public enum AccessibilityLevel
{
    Unknown,
    Limited,
    PartiallyAccessible,
    HighlyAccessible
}
