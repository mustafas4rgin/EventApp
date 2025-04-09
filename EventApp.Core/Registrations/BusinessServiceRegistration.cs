using EventApp.Core.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Core.Registrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        ServiceRegistrationProvider.RegisterServices(services);

        return services;
    }
}