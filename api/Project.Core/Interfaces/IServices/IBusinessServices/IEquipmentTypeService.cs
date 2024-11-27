using Project.Core.DTO.EquipmentDTO;

namespace Project.Core.Interfaces.IServices.IBusinessServices
{
    public interface IEquipmentTypeService : IBaseCrudService<GetEquipmentTypeDTO, AddEquipmentTypeDTO, UpdateEquipmentTypeDTO>{}
}