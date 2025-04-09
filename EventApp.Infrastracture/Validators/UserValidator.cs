using EventApp.Data.Entities;
using FluentValidation;

namespace EventApp.Infrastracture.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.CreatedAt)
            .NotEmpty()
            .WithMessage("Created at cannot be null.");

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Invalid email address.")
            .Length(6,70)
            .WithMessage("Email must be between 6-70 characters.");

        RuleFor(u => u.Phone)
            .NotEmpty()
            .Length(5,50)
            .WithMessage("Phone number must be bewteen 5-50 characters.");

        RuleFor(u => u.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is must.");
        
        RuleFor(u => u.PasswordSalt)
            .NotEmpty()
            .WithMessage("Password is must.");

        RuleFor(u => u.RoleId)
            .GreaterThan(0)
            .WithMessage("ID value must be greater than zero");
    }
}