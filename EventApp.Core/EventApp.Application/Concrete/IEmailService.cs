namespace EventApp.Application.Concrete
{
    public interface IEmailService
    {
        Task<IServiceResult> SendEmailAsync(string to, string subject, string body);
        Task<IServiceResult> ResetPasswordAsync(string email,string token);
    }
}