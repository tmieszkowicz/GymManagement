namespace GymManagement.MediatorLibrary;

public class Mediator : IMediator
{
    private readonly Func<Type, object> _serviceResolver;
    private readonly IDictionary<Type, Type> _handlerDetails;

    public Mediator(Func<Type, object> serviceResolver, IDictionary<Type, Type> handlerDetails)
    {
        _serviceResolver = serviceResolver;
        _handlerDetails = handlerDetails;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        where TResponse : ResultBase
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        if (!_handlerDetails.ContainsKey(requestType))
        {
            throw new Exception($"No handler to handle request of type: {requestType.Name}");
        }

        var handlerType = _handlerDetails[requestType];
        var handler = _serviceResolver(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"No service resolved for handler type: {handlerType.FullName}");
        }

        // return await ((Ihandler<IRequest<TResponse>, TResponse>)handler).Handle(request);
        // unable to cast type this to type that
        // sort of tricky situation
        // what we could do is to create a base class and wrappers around and redirect the request handling
        // instead we are going to use reflection

        var method = handler.GetType().GetMethod(
            "Handle",
            new[] { requestType }
        );

        if (method == null)
        {
            throw new InvalidOperationException($"Handler for request type {requestType.FullName} does not contain a matching Handle method.");
        }

        var invokeResult = method.Invoke(handler, new[] { request });
        if (invokeResult == null)
        {
            throw new InvalidOperationException($"Handle method on {handler.GetType().FullName} returned null.");
        }

        return await (Task<TResponse>)invokeResult;
    }
}
