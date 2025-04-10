namespace EventApp.Domain.DTOs;

public class UserDTO
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Name { get; set; } = null!;
}