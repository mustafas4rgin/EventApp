using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IUserService : IService<User>
{
    Task<IServiceResult<IEnumerable<User>>> GetUsersWithIncludesAsync(string? include);
    Task<IServiceResult<User>> GetUserWithIncludeAsync(int id, string? include);
    Task<IServiceResult> DeleteUserAsync(int id);
    Task<IServiceResult> UpdateUserAsync(User User);
}