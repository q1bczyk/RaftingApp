using Project.Core.DTO.EquipmentDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.BusinessService
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IEquipmentTypeRepository _repository;
        private readonly IBaseMapper<AddEquipmentTypeDTO, EquipmentType> _toModelMapper;
        private readonly IBaseMapper<EquipmentType, GetEquipmentTypeDTO> _toDTOMapper;
        private readonly IFileService _fileService;
        public EquipmentTypeService(IEquipmentTypeRepository repository, IBaseMapper<AddEquipmentTypeDTO, EquipmentType> toModelMapper, IBaseMapper<EquipmentType, GetEquipmentTypeDTO> toDTOMapper, IFileService fileService)
        {
            _repository = repository;
            _toModelMapper = toModelMapper;
            _toDTOMapper = toDTOMapper;
            _fileService = fileService;
        }
        public async Task<GetEquipmentTypeDTO> AddEquipmentType(AddEquipmentTypeDTO addEquipmentTypeDTO)
        {
            var photoUrl = await _fileService.Upload(addEquipmentTypeDTO.file, addEquipmentTypeDTO.TypeName);
            var newEquipmentType = _toModelMapper.MapToModel(addEquipmentTypeDTO);
            newEquipmentType.PhotoUrl = photoUrl;
            newEquipmentType.TypeName = newEquipmentType.TypeName.ToLower();
            var addedEquipmentType = await _repository.Create(newEquipmentType);
            return _toDTOMapper.MapToModel(addedEquipmentType);
        }

        public Task<GetEquipmentTypeDTO> GetEquipmentType(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GetEquipmentTypeDTO[]> GetEquipmentTypes()
        {
            throw new NotImplementedException();
        }

        public Task<GetEquipmentTypeDTO> UpdateEquipmentType(AddEquipmentTypeDTO updateEquipmentTypeDTO)
        {
            throw new NotImplementedException();
        }
    }
}