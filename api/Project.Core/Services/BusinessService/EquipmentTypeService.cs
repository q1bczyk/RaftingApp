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
    AddEquipmentTypeDTO,
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
            var newEquipmentType = await UploadFileAndMap(addEquipmentTypeDTO);
            var addedEquipmentType = await _repository.Create(newEquipmentType);
            return _toDTOMapper.MapToModel(addedEquipmentType);
        }

        public override async Task Delete(string id)
        {
            var dataToDelete = await _repository.GetById(id);
            await _fileService.Delete(dataToDelete.PhotoUrl);
            await _repository.Delete(dataToDelete);
        }

        public override async Task<GetEquipmentTypeDTO> Update(AddEquipmentTypeDTO updateEquipmentTypeDTO, string id)
        {
            var dataToEdit = await PrepareDataToEdit(updateEquipmentTypeDTO, id);
            var editedData = await _repository.Update(dataToEdit);
            return _toDTOMapper.MapToModel(editedData);
        }

        private async Task<EquipmentType> UploadFileAndMap(AddEquipmentTypeDTO addEquipmentTypeDTO)
        {
            var photoUrl = await _fileService.Upload(addEquipmentTypeDTO.file, addEquipmentTypeDTO.TypeName);
            var newEquipmentType = _toModelMapper.MapToModel(addEquipmentTypeDTO);
            newEquipmentType.PhotoUrl = photoUrl;
            return newEquipmentType;
        }

        private async Task<EquipmentType> PrepareDataToEdit(AddEquipmentTypeDTO updateEquipmentTypeDTO, string id)
        {
            var dataToEdit = await _repository.GetById(id);

            if (updateEquipmentTypeDTO.file != null)
            {
                await _fileService.Delete(dataToEdit.PhotoUrl);
                var photoUrl = await _fileService.Upload(updateEquipmentTypeDTO.file, updateEquipmentTypeDTO.TypeName);
                dataToEdit.PhotoUrl = photoUrl;
            }

            dataToEdit.TypeName = updateEquipmentTypeDTO.TypeName;
            dataToEdit.MaxParticipants = updateEquipmentTypeDTO.MaxParticipants;
            dataToEdit.MinParticipants = updateEquipmentTypeDTO.MinParticipants;
            dataToEdit.PricePerPerson = updateEquipmentTypeDTO.PricePerPerson;
            dataToEdit.Quantity = updateEquipmentTypeDTO.Quantity;

            return dataToEdit;
        }
    }
}