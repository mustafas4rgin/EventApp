using EventApp.Domain.Entities;

namespace EventApp.Application.Concrete;

public interface IRoleService : IService<Role>
{
    Task<IServiceResult<IEnumerable<Role>>> GetRolesWithUsers();
    Task<IServiceResult<Role>> GetRoleWithUser(int id);
}