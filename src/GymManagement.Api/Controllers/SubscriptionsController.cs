using GymManagement.Application.Subscriptions.Commands;
using GymManagement.Contracts.Subscriptions;
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

        Result<Guid> createSubscriptionResult = await _mediator.Send(command);

        //TODO: thats a possibility right :/
        // return createSubscriptionResult.Match(
        //     guid => Ok(new SubscriptionResponse(guid, request.SubscriptionType)),
        //     errors => Problem());

        if (createSubscriptionResult.IsFailure)
        {
            return Problem();
        }

        var response = new SubscriptionResponse(createSubscriptionResult.Value, request.SubscriptionType);

        return Ok(response);
    }
}
