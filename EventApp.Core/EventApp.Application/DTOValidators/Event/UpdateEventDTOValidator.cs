using EventApp.Domain.DTOs;
using FluentValidation;

namespace EventApp.Application.DTOValidators.Event;

public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDTO>
{
    public UpdateEventDTOValidator()
    {
        RuleFor(e => e.Title)
        .Length(3,12)
        .WithMessage("Title should be between 3-12 characters.");

        RuleFor(e => e.Description)
        .Length(10,100)
        .WithMessage("Description must be between 10-100 characters.");
    }
}