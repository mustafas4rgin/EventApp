using EventApp.Application.Concrete;
using EventApp.Application.Results;
using EventApp.Core.Services;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class UserEventRelService : Service<EventUserRel>, IUserEventRel
{
    private readonly IRepository<EventUserRel> _repository;
    private readonly IValidator<EventUserRel> _validator;
    public UserEventRelService(IValidator<EventUserRel> validator, IRepository<EventUserRel> repository) : base(repository, validator)
    {
        _validator = validator;
        _repository = repository;
    }
    public async Task<IServiceResult> CreateEventUserRelAsync(int eventId, int userId)
    {
        if (eventId <= 0 || userId <= 0)
            return new RawErrorResult("Invalid event or user ID.");
        var existingRel = await _repository.GetAll()
                            .FirstOrDefaultAsync(e => e.EventId == eventId && e.UserId == userId);
        if (existingRel != null)
            return new RawErrorResult("User already booked this event.");

        try
        {
            var eventUserRel = new EventUserRel
            {
                EventId = eventId,
                UserId = userId
            };

            await _repository.AddAsync(eventUserRel);
            await _repository.SaveChangesAsync();

            return new RawSuccessResult("Relation created successfully.");
        }
        catch (Exception ex)
        {
            return new RawErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> DeleteEventUserRelAsync(int eventId, int userId)
    {
        try
        {
            var eventUserRel = await _repository.GetAll()
                            .FirstOrDefaultAsync(e => e.EventId == eventId && e.UserId == userId);

            if (eventUserRel == null)
                return new RawErrorResult("No relation found.");

            await _repository.DeleteByIdAsync(eventUserRel.Id);
            await _repository.SaveChangesAsync();

            return new RawSuccessResult("Relation deleted successfully.");
        }
        catch (Exception ex)
        {
            return new RawErrorResult(ex.Message);
        }
    }
}