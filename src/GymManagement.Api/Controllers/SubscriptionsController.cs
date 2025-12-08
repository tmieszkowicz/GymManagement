using GymManagement.Application.Subscriptions.Commands;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.Subscriptions;
using GymManagement.MediatorLibrary;

using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var command = new CreateSubscriptionCommand(
            request.SubscriptionType.ToString(),
            request.AdminId);

        Result<Subscription> createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Map<IActionResult>(
            OnSuccess: subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            OnFailure: error => Problem(title: error.Code, detail: error.Description)
        );
    }
}
