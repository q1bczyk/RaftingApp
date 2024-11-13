using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class ReservationEquipment : BaseEntity
    {
        [Required]
        public string EquipmentId { get; set; }
        [Required]
        public string ReservationId { get; set; }
        public Equipment Equipment { get; set; } = null!;
        public Reservation Reservation { get; set; } = null!;
    }
}