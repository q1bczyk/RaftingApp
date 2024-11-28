using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class ReservationEquipment : BaseEntity
    {
        [Required]
        public string EquipmentTypeId { get; set; }
        [Required]
        public string ReservationId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public EquipmentType EquipmentType { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
    }
}