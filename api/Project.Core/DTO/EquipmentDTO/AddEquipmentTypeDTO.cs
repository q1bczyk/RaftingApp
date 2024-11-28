using Microsoft.AspNetCore.Http;

namespace Project.Core.DTO.EquipmentDTO
{
    public class AddEquipmentTypeDTO : BaseEquipmentTypeDTO
    {
        public IFormFile? file { get; set; }
    }
}