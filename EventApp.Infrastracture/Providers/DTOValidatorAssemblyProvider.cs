using EventApp.Infrastracture.DTOValidators;
using EventApp.Infrastracture.DTOValidators.Event;
using EventApp.Infrastracture.DTOValidators.Role;
using EventApp.Infrastracture.Validators;

namespace EventApp.Infrastracture.Providers;
public class DTOValidatorAssemblyProvider
{
    public static Type[] GetValidatorAssemblies()
    {
        return new[]
        {
            typeof(EventDTOValidator),
            typeof(UpdateEventDTOValidator),
            typeof(RoleDTOValidator),
            typeof(UpdateRoleDTOValidator)
        };
    }
}