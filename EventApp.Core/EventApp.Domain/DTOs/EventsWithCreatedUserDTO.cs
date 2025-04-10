namespace EventApp.Domain.DTOs;

public class GetEventsWithCreatedUserDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public UserListDTO CreatedByUser { get; set; } = null!;
}