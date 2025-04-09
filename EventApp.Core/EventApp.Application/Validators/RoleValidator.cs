using EventApp.Domain.Entities;
using FluentValidation;

namespace EventApp.Application.Validators;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator()
    {
        RuleFor(r => r.CreatedAt)
            .NotNull()
            .WithMessage("Created at cannot be null.");

        RuleFor(r => r.name)
            .NotEmpty()
            .WithMessage("Name must be filled.")
            .Length(3,80)
            .WithMessage("Name must be between 3-80 characters");

    }
}