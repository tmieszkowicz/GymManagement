using GymManagement.Shared.Mediator;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommand(string subscriptionType, Guid adminId) : IRequest<Guid>;