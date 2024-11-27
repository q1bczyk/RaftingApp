using Microsoft.AspNetCore.Http;

namespace Project.Core.DTO.EquipmentDTO
{
    public class UpdateEquipmentTypeDTO : BaseEquipmentDTO
    {
        public IFormFile file { get; set; }
    }
}