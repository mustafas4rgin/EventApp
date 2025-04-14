using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IAuthService
{
    Task<IServiceResult> RegisterAsync(UserDTO dto);
    Task<IServiceResult<string>> LoginAsync(LoginDTO dto);
    IServiceResult<string> GenerateJwtToken(User user);
}