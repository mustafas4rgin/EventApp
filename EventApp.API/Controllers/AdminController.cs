using EventApp.Application.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPut("updaterole/{userId}")]
        public async Task<IActionResult> UpdateUserRole(int userId, [FromQuery]int roleId)
        {
            var result = await _adminService.UpdateUserRoleAsync(userId, roleId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var result = await _adminService.DashboardSummaryAsync();

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
