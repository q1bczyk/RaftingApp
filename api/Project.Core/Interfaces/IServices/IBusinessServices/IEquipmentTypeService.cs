using Project.Core.DTO.EquipmentDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IEquipmentTypeService
    {
        public Task<GetEquipmentTypeDTO> AddEquipmentType(AddEquipmentTypeDTO addEquipmentTypeDTO);
        public Task<GetEquipmentTypeDTO> UpdateEquipmentType(AddEquipmentTypeDTO updateEquipmentTypeDTO);
        public Task<GetEquipmentTypeDTO[]> GetEquipmentTypes();
        public Task<GetEquipmentTypeDTO> GetEquipmentType(string id);
    }
}