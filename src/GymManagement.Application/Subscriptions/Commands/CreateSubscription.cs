using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.MediatorLibrary;
using GymManagement.Result;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(string SubscriptionType, Guid AdminId) : IRequest<Result<Subscription>>;

public class CreateSubscriptionCommandHandler : IHandler<CreateSubscriptionCommand, Result<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    // private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    // public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        // _unitOfWork = unitOfWork;
    }

    public async Task<Result<Subscription>> Handle(CreateSubscriptionCommand request)
    {
        // Create a subscription
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            SubscriptionType = request.SubscriptionType
        };

        // Add it to the database
        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        // await _unitOfWork.CommitChangesAsync();

        // Return subscription
        return Result<Subscription>.Success(subscription);
    }
}
