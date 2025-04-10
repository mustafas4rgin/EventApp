using EventApp.Domain.DTOs;
using FluentValidation;

namespace EventApp.Application.DTOValidators;

public class UserEventRelDTOValidator : AbstractValidator<EventuserRelDTO>
{
    public UserEventRelDTOValidator()
    {
        RuleFor(eur => eur.EventId)
        .NotNull()
        .WithMessage("Id value cannot be null.")
        .GreaterThan(0)
        .WithMessage("Id value must be greater than zero.");
        
        RuleFor(eur => eur.UserId)
        .NotNull()
        .WithMessage("Id value cannot be null.")
        .GreaterThan(0)
        .WithMessage("Id value must be greater than zero.");
    }
}