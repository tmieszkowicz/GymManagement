using Microsoft.Extensions.DependencyInjection;
using GymManagement.Shared.Mediator;
using System.Reflection;

namespace GymManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(Assembly.GetExecutingAssembly());

        return services;
    }
}
