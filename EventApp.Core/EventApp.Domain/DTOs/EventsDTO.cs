namespace EventApp.Domain.DTOs;

public class EventsDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public UserListDTO CreatedByUser { get; set; } = null!;
    public List<UserListDTO> BookedUsers { get; set; } = null!;
}