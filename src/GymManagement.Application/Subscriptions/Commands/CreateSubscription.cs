using GymManagement.MediatorLibrary;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(string subscriptionType, Guid adminId) : IRequest<Guid>;

public class CreateSubscriptionCommandHandler : IHandler<CreateSubscriptionCommand, Guid>
{
    public Task<Guid> Handle(CreateSubscriptionCommand request)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
