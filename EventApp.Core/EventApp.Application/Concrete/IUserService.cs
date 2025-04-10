using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IUserService
{
    Task<IServiceResult<User>> LoginAsync(LoginDTO dto);
    Task<IServiceResult> RegisterAsync(UserDTO dto);
    Task<IServiceResult<IEnumerable<User>>> GetAllUsersWithBookedEventsAsync();
    Task<IServiceResult<IEnumerable<User>>> GetAllUsersWithRoleAsync();
}