using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IAuthService
{
    Task<IServiceResult> RegisterAsync(UserDTO dto);
    Task<IServiceResult<User>> LoginAsync(LoginDTO dto);
    IServiceResult<string> GenerateJwtToken(User user);
    Task<IServiceResult> ForgotPasswordAsync(string email);
    Task<IServiceResult> ResetPasswordAsync(string email, string newPassword);
}