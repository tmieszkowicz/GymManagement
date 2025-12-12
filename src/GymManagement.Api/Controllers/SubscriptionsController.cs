using GymManagement.Application.Subscriptions.Commands;
using GymManagement.Application.Subscriptions.Queries;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.Subscriptions;
using GymManagement.MediatorLibrary;
using GymManagement.Result;

using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;
using SubscriptionType = GymManagement.Contracts.Subscriptions.SubscriptionType;

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
        var subscriptionType =
            DomainSubscriptionType.FromName(request.SubscriptionType.ToString());

        if (subscriptionType is null)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription type.");
        }

        var command = new CreateSubscriptionCommand(
            subscriptionType,
            request.AdminId);

        Result<Subscription> createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Map<IActionResult>(
            OnSuccess: subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            OnFailure: error => Problem(title: error.Code, detail: error.Description)
        );
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        Result<Subscription> getSubscriptionResult = await _mediator.Send(query);

        return getSubscriptionResult.Map<IActionResult>(
            OnSuccess: subscription => Ok(new SubscriptionResponse(subscription.Id, Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
            OnFailure: error => Problem(title: error.Code, detail: error.Description)
        );
    }
}
