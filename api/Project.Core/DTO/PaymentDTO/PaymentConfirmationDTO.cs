using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.PaymentDTO
{
    public class PaymentConfirmationDTO
    {
        [Required]
        public string StripeId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}