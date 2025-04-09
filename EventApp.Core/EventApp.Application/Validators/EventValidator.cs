using EventApp.Domain.Entities;
using FluentValidation;

namespace EventApp.Application.Validators;

public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
    {
        RuleFor(e => e.CreatedAt)
            .NotEmpty()
            .WithMessage("Created at cannot be null.");

        RuleFor(e => e.CreatedByUserId)
            .NotEmpty()
            .WithMessage("CreatedByUserID value cannot be null")
            .GreaterThan(0)
            .WithMessage("CreatedByUserID value must be greater than zero.");

        RuleFor(e => e.Description)
            .Length(10,100)
            .WithMessage("Description must be between 10-100 characters.");

        RuleFor(e => e.Title)
            .Length(3,12)
            .WithMessage("Title must be bewteen 3-12 characters.");
    }
}