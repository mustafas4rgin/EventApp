using EventApp.Application.Concrete;
using EventApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Core.Providers;

public class ServiceRegistrationProvider
{
    public static void RegisterServices(IServiceCollection services)
    {
        var servicesToRegister = new (Type Interface, Type Implementation)[]
        {
            (typeof(IService<>),typeof(Service<>)),
        };
        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}