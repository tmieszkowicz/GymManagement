using GymManagement.Result;

namespace GymManagement.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static readonly Error GymAlreadyExists =
        Error.Conflict(
            "A gym with the specified ID already exists in this subscription.");

    public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows =
        Error.Failure(
            "This subscription does not allow adding more gyms.");

    public static readonly Error GymNotFound =
        Error.NotFound(
            "The specified gym does not exist in this subscription.");
}
