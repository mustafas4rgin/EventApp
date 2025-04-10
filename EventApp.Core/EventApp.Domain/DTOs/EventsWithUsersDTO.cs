using EventApp.Domain.Entities;

namespace EventApp.Domain.DTOs;

public class EventsWithUsersDTO
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<UserListDTO> BookedUsers { get; set; } = null!;
}