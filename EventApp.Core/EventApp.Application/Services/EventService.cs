using EventApp.Application.Concrete;
using EventApp.Application.Results;
using EventApp.Core.Services;
using EventApp.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class EventService : Service<Event>, IEventService
{
    private readonly IRepository<Event> _repository;
    private readonly IValidator<Event> _validator;
    public EventService(IRepository<Event> repository, IValidator<Event> validator) : base(repository, validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IServiceResult<Event>> GetByIdWithIncludesAsync(int id, string? include)
    {
        try
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(x => x.Trim().ToLower()))
                {
                    if (inc == "creator")
                        query = query.Include(e => e.CreatedByUser);
                    else if (inc == "bookedusers")
                        query = query.Include(e => e.BookedUsers);
                }
            }

            var eventEntity = await query.FirstOrDefaultAsync(e => e.Id == id);

            if (eventEntity == null)
                return new ErrorResult<Event>("No event found.");

            return new SuccessResult<Event>("Event found.", eventEntity);
        }

        catch (Exception ex)
        {
            return new ErrorResult<Event>(ex.Message);
        }
    }
    public async Task<IServiceResult<IEnumerable<Event>>> GetEventsWithIncludesAsync(string? include)
    {
        try
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var inc in includes.Select(x => x.Trim().ToLower()))
                {
                    if (inc == "creator")
                        query = query.Include(e => e.CreatedByUser);

                    else if (inc == "bookedusers")
                        query = query.Include(e => e.BookedUsers);
                }
            }

            var events = await query.ToListAsync();

            if (!events.Any())
                return new ErrorResult<IEnumerable<Event>>("There is no event");

            return new SuccessResult<IEnumerable<Event>>("Events: ", events);
        }
        catch (Exception ex)
        {
            return new ErrorResult<IEnumerable<Event>>(ex.Message);
        }
    }

}