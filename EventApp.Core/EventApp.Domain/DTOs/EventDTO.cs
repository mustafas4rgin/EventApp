namespace EventApp.Domain.DTOs;

public class EventDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CreatedByUserId { get; set; }
}