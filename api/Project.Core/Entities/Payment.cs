using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string ReservationId { get; set; }
        [Required]
        public Reservation Reservation { get; set; } = null!;
    }
}