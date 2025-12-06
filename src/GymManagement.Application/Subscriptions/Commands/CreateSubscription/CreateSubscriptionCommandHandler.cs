using GymManagement.Shared.Mediator;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    public Task<Guid> Handle(CreateSubscriptionCommand request)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
