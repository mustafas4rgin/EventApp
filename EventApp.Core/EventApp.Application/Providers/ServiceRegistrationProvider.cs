using EventApp.Application.Concrete;
using EventApp.Application.Services;
using EventApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Application.Providers;

public class ServiceRegistrationProvider
{
    public static void RegisterServices(IServiceCollection services)
    {
        var servicesToRegister = new (Type Interface, Type Implementation)[]
        {
            (typeof(IService<>),typeof(Service<>)),
            (typeof(IUserService),typeof(UserService)),
            (typeof(IEventService),typeof(EventService)),
            (typeof(IRoleService),typeof(RoleService))
        };
        foreach (var service in servicesToRegister)
        {
            services.AddTransient(service.Interface, service.Implementation);
        }
    }
}