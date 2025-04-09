namespace EventApp.Domain.DTOs;

public class UpdateEventDTO 
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}