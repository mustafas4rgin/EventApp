using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IEventService : IService<Event>
{
    Task<IServiceResult<IEnumerable<Event>>> GetEventsWithIncludesAsync(string? includes);
    Task<IServiceResult<Event>> GetByIdWithIncludesAsync(int id, string? includes);
}