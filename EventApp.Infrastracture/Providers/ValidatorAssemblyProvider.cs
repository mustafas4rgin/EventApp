using EventApp.Infrastracture.Validators;

namespace EventApp.Infrastracture.Providers;
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