using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete
{
    public interface IUserEventRel : IService<EventUserRel>
    {
        Task<IServiceResult> DeleteEventUserRelAsync(int eventId, int userId);
        Task<IServiceResult> CreateEventUserRelAsync(int eventId, int userId);
    }
}