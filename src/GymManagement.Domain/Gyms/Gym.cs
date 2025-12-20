using GymManagement.Domain.Rooms;
using GymManagement.Result;

namespace GymManagement.Domain.Gyms;

public class Gym
{
    private readonly int _maxRooms;

    public Guid Id { get; }
    private readonly List<Guid> _roomsIds = new();
    private readonly List<Guid> _trainerIds = new();

    public string Name { get; init; } = null!;
    public Guid SubscriptionId { get; init; }

    public Gym(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null)
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
    }

    public Result<Error> AddRoom(Room room)
    {
        if (_roomsIds.Contains(room.Id))
            Result<Error>.Failure(GymErrors.RoomAlreadyExists);

        if (_roomsIds.Count >= _maxRooms)
            Result<Error>.Failure(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows);

        _roomsIds.Add(room.Id);

        return Result<Error>.Success();
    }
}
