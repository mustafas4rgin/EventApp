using EventApp.Application.DTOValidators;
using EventApp.Application.DTOValidators.Event;
using EventApp.Application.DTOValidators.Role;

namespace EventApp.Application.Providers;
public class DTOValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(EventDTOValidator),
            typeof(UpdateEventDTOValidator),
            typeof(RoleDTOValidator),
            typeof(UpdateRoleDTOValidator),
            typeof(UserEventRelDTOValidator),
            typeof(LoginDtoValidator)
        };
    }
}