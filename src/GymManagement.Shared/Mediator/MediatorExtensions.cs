using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace GymManagement.Shared.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddSingleton<IMediator, Mediator>();

        var handlerTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

        foreach (var handler in handlerTypes)
        {
            foreach (var iface in handler.GetInterfaces()
                     .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            {
                services.AddTransient(iface, handler);
            }
        }

        return services;
    }
}
