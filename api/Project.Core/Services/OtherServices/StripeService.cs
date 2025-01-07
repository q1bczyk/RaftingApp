using api;
using Microsoft.Extensions.Options;
using Project.Core.Config;
using Project.Core.DTO.PaymentDTO;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices.IOtherServices;
using Stripe;

namespace Project.Core.Services.OtherServices
{
    public class StripeService : IStripeService
    {
        private string _secretKey { get; set; }
        private ISettingsRepository _settingsRepository;

        public StripeService(IOptions<StripeConfig> stripeConfig, ISettingsRepository settingsRepository)
        {
            _secretKey = stripeConfig.Value.SecretKey;
            _settingsRepository = settingsRepository;
        }

        public async Task<string> CreatePaymentIntent(BlikPaymentDTO blikPaymentDTO)
        {
            StripeConfiguration.ApiKey = _secretKey;

            var options = new PaymentIntentCreateOptions
            {
                Amount = blikPaymentDTO.Amount,
                Currency = "pln",
                PaymentMethodTypes = new List<string> { "blik" },
                PaymentMethodData = new PaymentIntentPaymentMethodDataOptions
                {
                    Type = "blik",
                    Metadata = new Dictionary<string, string>
                    {
                        { "blik_code", blikPaymentDTO.BlikCode } // Przekazanie kodu BLIK jako metadanych
                    }
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent.ClientSecret;
        }

        public async Task Refund(string paymentId, int amount, DateTime bookingDate)
        {
            StripeConfiguration.ApiKey = _secretKey;
            
            var systemSettings = await _settingsRepository.GetSettingsAsync();
            DateTime latestRefundDate = bookingDate.AddDays(-systemSettings.DayLatestBookingTime);
            DateTime today = DateTime.UtcNow;

            if(today > latestRefundDate) return;

            var refoundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentId,
                Amount = amount
            };

            var refundService = new RefundService();
            Refund refund = await refundService.CreateAsync(refoundOptions);
        }
    }
}