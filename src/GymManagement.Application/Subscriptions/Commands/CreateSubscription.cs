using GymManagement.Application.Subscriptions.Errors;
using GymManagement.MediatorLibrary;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(string subscriptionType, Guid adminId) : IRequest<Result<Guid>>;

public class CreateSubscriptionCommandHandler : IHandler<CreateSubscriptionCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateSubscriptionCommand request)
    {
        // return SubscriptionErrors.SubscriptionCreationFailed;

        Guid guid = Guid.NewGuid();

        return Task.FromResult(Result<Guid>.Success(guid));
    }
}
