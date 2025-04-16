using EventApp.Application.Concrete;
using EventApp.Application.Results;
using EventApp.Domain.DTOs;
using EventApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Application.Services;

public class AdminService : IAdminService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;

    public AdminService(IRepository<Role> roleRepository, IRepository<Event> eventRepository, IRepository<User> userRepository)
    {
        _roleRepository = roleRepository;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }

    public async Task<IServiceResult> UpdateUserRoleAsync(int userId, int roleId)
    {
        if (userId <= 0 || roleId <= 0)
            return new RawErrorResult("Invalid user or role ID.");

        var user = await _userRepository.GetAll()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return new RawErrorResult("User not found.");

        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role == null)
            return new RawErrorResult("Role not found.");

        user.RoleId = roleId;

        try
        {
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return new RawSuccessResult("User role updated successfully.");
        }
        catch (Exception ex)
        {
            return new RawErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult<DashboardSummaryDTO>> DashboardSummaryAsync()
    {
        var totalEvents = await _eventRepository.GetAll().CountAsync();
        var totalUsers = await _userRepository.GetAll().CountAsync();
        var totalBookings = await _eventRepository.GetAll()
            .SelectMany(e => e.BookedUsers)
            .CountAsync();

        if (totalEvents == 0 && totalUsers == 0)
            return new ErrorResult<DashboardSummaryDTO>("No data available.");
        if (totalEvents < 0 || totalUsers < 0)
            return new ErrorResult<DashboardSummaryDTO>("Invalid data.");
        var summary = new DashboardSummaryDTO
        {
            EventCount = totalEvents,
            UserCount = totalUsers,
            ResarvationCount = totalBookings
        };

        return new SuccessResult<DashboardSummaryDTO>("Dashboard summary retrieved successfully.", summary);
    }
}