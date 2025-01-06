using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class Payment : BaseEntity 
    {
        [Required]
        public string StripeId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string ReservationId { get; set; }
        [Required]
        public Reservation Reservation { get; set; } = null!;
    }
}