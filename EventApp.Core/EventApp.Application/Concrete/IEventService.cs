using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IEventService : IService<Event>
{
    Task<IServiceResult<IEnumerable<Event>>> GetEventsWithCreator();
    Task<IServiceResult<IEnumerable<Event>>> GetEventsWithBookedUsers();
}