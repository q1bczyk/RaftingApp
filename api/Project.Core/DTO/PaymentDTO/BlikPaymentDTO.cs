using System.ComponentModel.DataAnnotations;

namespace Project.Core.DTO.PaymentDTO
{
    public class BlikPaymentDTO : BasePaymentDTO
    {
        [Required, MinLength(6), MaxLength(6)]
        public string BlikCode { get; set; }
    }
}