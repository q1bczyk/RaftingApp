using Project.Core.DTO.EquipmentDTO;
using Project.Core.Entities;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IBusinessServices;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Core.Services.BusinessService
{
    public class EquipmentTypeService : BaseCrudService<
    GetEquipmentTypeDTO, 
    AddEquipmentTypeDTO, 
    UpdateEquipmentTypeDTO,  
    EquipmentType, 
    IEquipmentTypeRepository>, 
    IEquipmentTypeService
    {
        private readonly IFileService _fileService;
        public EquipmentTypeService(IEquipmentTypeRepository repository, IBaseMapper<AddEquipmentTypeDTO, EquipmentType> toModelMapper, IBaseMapper<EquipmentType, GetEquipmentTypeDTO> toDTOMapper, IFileService fileService) : base(repository, toModelMapper, toDTOMapper)
        {
            _fileService = fileService;
        }
        public override async Task<GetEquipmentTypeDTO> Create(AddEquipmentTypeDTO addEquipmentTypeDTO)
        {
            var photoUrl = await _fileService.Upload(addEquipmentTypeDTO.file, addEquipmentTypeDTO.TypeName);
            var newEquipmentType = _toModelMapper.MapToModel(addEquipmentTypeDTO);
            newEquipmentType.PhotoUrl = photoUrl;
            newEquipmentType.TypeName = newEquipmentType.TypeName.ToLower();
            var addedEquipmentType = await _repository.Create(newEquipmentType);
            return _toDTOMapper.MapToModel(addedEquipmentType);
        }

        // public override async Task<GetEquipmentTypeDTO> Update(UpdateEquipmentTypeDTO updateEquipmentTypeDTO)
        // {

        // }
    }
}