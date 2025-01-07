using Project.Core.DTO.PaymentDTO;

namespace Project.Core.Interfaces.IServices.IOtherServices
{
    public interface IStripeService
    {
        Task<string> CreatePaymentIntent(BlikPaymentDTO blikPaymentDTO);
        Task Refund(string paymentId, int amount, DateTime bookingDate);
    }
}