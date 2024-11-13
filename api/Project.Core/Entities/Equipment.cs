using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class Equipment : BaseEntity
    {
        [Required]
        public string EquipmentTypeId { get; set; }
        [Required]
        public EquipmentType EquipmentType { get; set; } = null!;
        public IEnumerable<ReservationEquipment> ReservationEquipment { get; } = [];
    }
}