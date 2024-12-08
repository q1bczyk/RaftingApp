using Project.Core.DTO.SettingsDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;

namespace Project.Core.Services.BusinessService
{
    public class SettingsService : BaseCrudService<
    GetSettingsDTO,
    BaseSettingsDTO,
    BaseSettingsDTO,
    Settings,
    ISettingsRepository
    >, ISettingsService
    {
        public SettingsService(ISettingsRepository repository, IBaseMapper<BaseSettingsDTO, Settings> toModelMapper, IBaseMapper<Settings, GetSettingsDTO> toDTOMapper) : base(repository, toModelMapper, toDTOMapper)
        {
        }

        public async Task<GetSettingsDTO> GetSettings()
        {
            var settings = await _repository.GetSettingsAsync();
            return _toDTOMapper.MapToModel(settings);
        }
    }
}