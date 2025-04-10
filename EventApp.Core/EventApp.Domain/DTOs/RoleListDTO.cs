namespace EventApp.Domain.DTOs;

public class RoleListDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<UserListDTO> Users { get; set; } = null!;
}