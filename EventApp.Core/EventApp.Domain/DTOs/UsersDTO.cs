namespace EventApp.Domain.DTOs;

public class UsersDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<EventsDTO> BookedEvents { get; set; } = null!;
    public RoleDTO Role { get; set; } = null!;
}