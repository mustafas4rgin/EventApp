using EventApp.Data.DTOs;
using FluentValidation;

namespace EventApp.Infrastracture.DTOValidators.Role;

public class UpdateRoleDTOValidator : AbstractValidator<UpdateRoleDTO>
{
    public UpdateRoleDTOValidator()
    {
        RuleFor(r => r.Name)
        .Length(3,80)
        .WithMessage("Name must be between 3-80 characters.");
    }
}