using Project.Core.DTO.DashboardDTO;

namespace Project.Core.Interfaces.IServices
{
    public interface IDashboardService
    {
        Task<DashboardDataDTO> GetDashboardData();
    }
}