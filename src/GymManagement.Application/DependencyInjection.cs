using System.Reflection;

using GymManagement.Application.Subscriptions.Commands;
using GymManagement.MediatorLibrary;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(ServiceLifetime.Scoped, typeof(CreateSubscriptionCommand));

        // var handlerDetails = new Dictionary<Type, Type>
        // {
        //     { typeof(CreateSubscriptionCommand), typeof(CreateSubscriptionCommandHandler) }
        // };

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, ServiceLifetime lifetime, params Type[] markers)
    {
        var handlerInfo = new Dictionary<Type, Type>();

        foreach (var marker in markers)
        {
            var assembly = marker.Assembly;
            var requests = GetClassesImplementingInterface(assembly, typeof(IRequest<>));
            var handlers = GetClassesImplementingInterface(assembly, typeof(IHandler<,>));

            requests.ForEach(x =>
            {
                handlerInfo[x] = handlers.SingleOrDefault(xx => x == xx.GetInterface("IHandler`2")!.GetGenericArguments()[0]);
            });

            var serviceDescriptor = handlers.Select(x => new ServiceDescriptor(x, x, lifetime));
            services.TryAdd(serviceDescriptor);
        }

        services.Add(new ServiceDescriptor(typeof(IMediator),
                    x => new Mediator((Type t) => x.GetRequiredService(t)!, handlerInfo),
                    lifetime));

        return services;
    }

    private static List<Type> GetClassesImplementingInterface(Assembly assembly, Type typeToMatch)
    {
        return assembly.ExportedTypes
            .Where(type =>
                !type.IsInterface &&
                !type.IsAbstract &&
                type.GetInterfaces()
                    .Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeToMatch))
            .ToList();
    }
}
