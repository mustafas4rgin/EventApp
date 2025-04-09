namespace EventApp.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int RoleId { get; set; }
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public ICollection<Event> BookedEvents {get; set;} = null!;
}