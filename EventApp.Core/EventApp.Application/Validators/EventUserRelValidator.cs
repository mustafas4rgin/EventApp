using EventApp.Domain.Entities;
using FluentValidation;

namespace EventApp.Application.Validators;

public class EventUserReulValidator : AbstractValidator<EventUserRel>
{
    public EventUserReulValidator()
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