using GymManagement.MediatorLibrary;

namespace GymManagement.Application.Subscriptions.Errors;

public static class SubscriptionErrors
{
    public static readonly Result<Guid> SubscriptionCreationFailed =
        new Error("CREATION_FAILED", "Failed to create subscription.");
    // Result<Guid>.Failure(new("CREATION_FAILED", "Failed to create subscription."));
}