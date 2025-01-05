using Microsoft.Extensions.Options;
using Project.Core.Config;
using Project.Core.DTO.PaymentDTO;
using Project.Core.Interfaces.IServices.IOtherServices;
using Stripe;

namespace Project.Core.Services.OtherServices
{
    public class StripeService : IStripeService
    {
        private string _secretKey { get; set; }

        public StripeService(IOptions<StripeConfig> stripeConfig)
        {
            _secretKey = stripeConfig.Value.SecretKey;
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
    }
}