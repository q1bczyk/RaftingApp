using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.PaymentDTO
{
    public class BasePaymentDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}