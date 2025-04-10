using EventApp.Domain.DTOs;
using FluentValidation;

namespace EventApp.Application.DTOValidators;

public class LoginDtoValidator : AbstractValidator<LoginDTO>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email adresi girin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı.");
    }
}