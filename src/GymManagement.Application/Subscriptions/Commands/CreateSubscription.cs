using GymManagement.Shared.Mediator;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(string subscriptionType, Guid adminId) : IRequest<Guid>;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    public Task<Guid> Handle(CreateSubscriptionCommand request)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
