using Microsoft.AspNetCore.Http;

namespace Project.Core.DTO.EquipmentDTO
{
    public class AddEquipmentTypeDTO : BaseEquipmentDTO
    {
        public IFormFile? file { get; set; }
    }
}