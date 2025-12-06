namespace GymManagement.Shared.Mediator;

using Microsoft.Extensions.DependencyInjection;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        return handler.Handle((dynamic)request);
    }
}
