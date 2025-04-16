using EventApp.Domain.DTOs;

namespace EventApp.Application.Concrete
{
    public interface IAdminService
    {
        Task<IServiceResult<DashboardSummaryDTO>> DashboardSummaryAsync();
        Task<IServiceResult> UpdateUserRoleAsync(int userId, int roleId);
    }
}