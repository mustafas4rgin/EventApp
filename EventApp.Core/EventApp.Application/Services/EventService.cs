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
    public EventService(IRepository<Event> repository, IValidator<Event> validator) : base(repository,validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<IServiceResult<IEnumerable<Event>>> GetEventsWithCreator()
    {
        var events =await _repository.GetAll()
                    .Include(e => e.CreatedByUser)
                    .ToListAsync();

        if(!events.Any() || events.Count() <= 0)
            return new ErrorResult<IEnumerable<Event>>("There is no event.");

        return new SuccessResult<IEnumerable<Event>>("Events:",events);
    }
    public async Task<IServiceResult<IEnumerable<Event>>> GetEventsWithBookedUsers()
    {
        var events = await _repository.GetAll()
                    .Include(e => e.BookedUsers)
                    .ToListAsync();

        if(!events.Any() || events.Count() <= 0)
            return new ErrorResult<IEnumerable<Event>>("There is no event.");

        return new SuccessResult<IEnumerable<Event>>("Events:",events);
    }
}