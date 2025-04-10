using EventApp.Domain.Entities;

namespace EventApp.Domain.DTOs;

public class UserWithRoleDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public RoleDTO Role { get; set; } = null!;
}