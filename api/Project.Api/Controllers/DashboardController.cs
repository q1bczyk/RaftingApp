using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.DashboardDTO;
using Project.Core.Interfaces.IServices;

namespace Project.Api.Controllers
{
    public class DashboardController : BaseApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Authorize(Policy = "Users")] 
        public async Task<ActionResult<DashboardDataDTO>> GetDashboardData()
        {
            var dashboardData = await _dashboardService.GetDashboardData();
            return Ok(dashboardData);
        }
    }
}