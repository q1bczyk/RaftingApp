namespace Project.Core.DTO.PaymentDTO
{
    public class PaymentDetailsDTO : BasePaymentDTO
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string StripeId { get; set; }
    }
}