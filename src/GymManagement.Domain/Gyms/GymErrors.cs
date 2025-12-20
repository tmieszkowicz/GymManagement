using GymManagement.Result;

namespace GymManagement.Domain.Gyms;

public static class GymErrors
{
    public static readonly Error RoomAlreadyExists =
        Error.Conflict(
            "A room with the specified ID already exists in this gym.");

    public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows =
        Error.Failure(
            "This gym cannot have more rooms than the subscription allows.");
}
