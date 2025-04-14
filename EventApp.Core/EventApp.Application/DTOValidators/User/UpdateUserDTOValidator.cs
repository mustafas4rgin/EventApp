using EventApp.Domain.DTOs;
using FluentValidation;

namespace EventApp.Application.DTOValidators;

public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    public UpdateUserDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

        RuleFor(u => u.Phone)
            .NotEmpty()
            .Length(5, 50)
            .WithMessage("Phone number must be bewteen 5-50 characters.");

        RuleFor(u => u.Username)
            .NotEmpty()
            .Length(5,25)
            .WithMessage("Username must be between. 5-25 characters.");
        
        RuleFor(u => u.Name)
            .NotEmpty()
            .Length(2,40)
            .WithMessage("Name must be between 2-40 characters.");
    }
}