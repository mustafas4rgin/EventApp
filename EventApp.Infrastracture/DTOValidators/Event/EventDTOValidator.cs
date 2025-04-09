using System.Data;
using EventApp.Data.DTOs;
using FluentValidation;

namespace EventApp.Infrastracture.DTOValidators.Event;

public class EventDTOValidator : AbstractValidator<EventDTO>
{
    public EventDTOValidator()
    {
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