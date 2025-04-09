using EventApp.Data.DTOs;
using FluentValidation;

namespace EventApp.Infrastracture.DTOValidators.Role;

public class RoleDTOValidator : AbstractValidator<RoleDTO>
{
    public RoleDTOValidator()
    {
        RuleFor(r => r.Name)
        .Length(3,80)
        .WithMessage("Name should be between 3-80 characters.");
    }
}