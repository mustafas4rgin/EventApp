namespace EventApp.Domain.Entities;

public class EventUserRel : EntityBase
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public Event Event { get; set; } = null!;
}