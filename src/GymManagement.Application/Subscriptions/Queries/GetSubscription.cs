using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.MediatorLibrary;

namespace GymManagement.Application.Subscriptions.Queries;

public record GetSubscriptionQuery(Guid SubscriptionId) : IRequest<Result<Subscription>>;

public class GetSubscriptionQueryHandler : IHandler<GetSubscriptionQuery, Result<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<Result<Subscription>> Handle(GetSubscriptionQuery query)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(query.SubscriptionId);

        return subscription is null
            ? Result<Subscription>.Failure(Error.NotFound("Subscription not found"))
            : Result<Subscription>.Success(subscription);
    }
}
