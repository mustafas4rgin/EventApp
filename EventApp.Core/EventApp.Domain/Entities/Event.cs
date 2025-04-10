using System.Text.Json.Serialization;

namespace EventApp.Domain.Entities;

public class Event : EntityBase
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<User> BookedUsers { get; set; } = null!;
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
}