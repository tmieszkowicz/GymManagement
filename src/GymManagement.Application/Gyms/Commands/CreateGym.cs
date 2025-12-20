using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.MediatorLibrary;
using GymManagement.Result;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<Result<Gym>>;

public class CreateGymCommandHandler : IHandler<CreateGymCommand, Result<Gym>>
{

    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Gym>> Handle(CreateGymCommand command)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Result<Gym>.Failure(Error.NotFound("Subscription is not found"));
        }

        var gym = new Gym(
            name: command.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsFailure)
        {
            return Result<Gym>.Failure(addGymResult.Error!);
        }

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymsRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result<Gym>.Success(gym);
    }
}
