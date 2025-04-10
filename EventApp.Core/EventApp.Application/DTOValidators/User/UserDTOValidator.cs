using EventApp.Domain.DTOs;
using FluentValidation;

namespace EventApp.Application.DTOValidators;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı.")
            .Matches(@"[A-Z]+").WithMessage("Şifre en az bir büyük harf içermeli.")
            .Matches(@"[a-z]+").WithMessage("Şifre en az bir küçük harf içermeli.")
            .Matches(@"\d+").WithMessage("Şifre en az bir rakam içermeli.")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]+").WithMessage("Şifre en az bir özel karakter içermeli.");

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