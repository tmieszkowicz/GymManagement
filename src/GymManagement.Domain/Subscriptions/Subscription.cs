using GymManagement.Domain.Gyms;
using GymManagement.Result;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    private readonly List<Guid> _gymIds = new();
    private readonly int _maxGyms;

    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; } = null!;

    public Guid AdminId { get; }

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();

        _maxGyms = GetMaxGyms();
    }

    public Result<Error> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return Result<Error>.Failure(
                SubscriptionErrors.GymAlreadyExists);

        if (_gymIds.Count >= GetMaxGyms())
            return Result<Error>.Failure(
                SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);

        _gymIds.Add(gym.Id);

        return Result<Error>.Success();
    }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }

    public Result<Error> RemoveGym(Guid gymId)
    {
        if (!_gymIds.Contains(gymId))
            return Result<Error>.Failure(SubscriptionErrors.GymNotFound);

        _gymIds.Remove(gymId);

        return Result<Error>.Success();
    }

    private Subscription()
    {
    }
}
