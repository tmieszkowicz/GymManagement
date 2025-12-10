using GymManagement.Result;

namespace GymManagement.Application.Subscriptions.Errors;

public static class SubscriptionErrors
{
    public static readonly Task<Result<Guid>> SubscriptionCreationFailed =
        Task.FromResult(Result<Guid>.Failure(new Error("CREATION_FAILED", "Failed to create subscription.")));
    // new Error("CREATION_FAILED", "Failed to create subscription.");
    // Result<Guid>.Failure(new("CREATION_FAILED", "Failed to create subscription."));
}