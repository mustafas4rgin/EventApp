using EventApp.Application.Validators;

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
        };
    }
}