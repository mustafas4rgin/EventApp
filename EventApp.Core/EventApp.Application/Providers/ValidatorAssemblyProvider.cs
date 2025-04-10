using EventApp.Application.DTOValidators;
using EventApp.Application.DTOValidators.Event;
using EventApp.Application.DTOValidators.Role;
using EventApp.Application.Validators;
using EventApp.Domain.DTOs;

namespace EventApp.Application.Providers;
public class ValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(UserValidator),
            typeof(RoleValidator),
            typeof(EventValidator),
            typeof(EventUserReulValidator)
        };
    }
}