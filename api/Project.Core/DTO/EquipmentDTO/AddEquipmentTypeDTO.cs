using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project.Core.DTO.EquipmentDTO
{
    public class AddEquipmentTypeDTO : BaseEquipmentDTO
    {
        [Required]
        public IFormFile file { get; set; }
    }
}