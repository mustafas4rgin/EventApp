using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;

namespace EventApp.Application.Helpers;

public class MappingHelper
{
    public static User UserMap(UserDTO dto)
    {
        HashingHelper.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            RoleId = 3,
            Phone = dto.Phone,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Username = dto.Username,
            CreatedAt = DateTime.UtcNow,
        };
    }
}