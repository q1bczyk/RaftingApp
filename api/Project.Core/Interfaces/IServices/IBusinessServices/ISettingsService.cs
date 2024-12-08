using Project.Core.DTO.SettingsDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface ISettingsService : IBaseCrudService<GetSettingsDTO, BaseSettingsDTO, BaseSettingsDTO>{
        Task<GetSettingsDTO> GetSettings();
    }
}