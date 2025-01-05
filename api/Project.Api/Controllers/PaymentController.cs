using Microsoft.AspNetCore.Mvc;
using Project.Core.DTO.BaseDTO;
using Project.Core.DTO.PaymentDTO;
using Project.Core.Interfaces.IServices.IOtherServices;

namespace Project.Api.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IStripeService _stripeService;

        public PaymentController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("BlikPaymentInit")]
        public async Task<ActionResult> CreatePaymentIntent(BlikPaymentDTO blikPaymentDTO)
        {
            string clientSecret = await _stripeService.CreatePaymentIntent(blikPaymentDTO);
            return Ok(new { ClientSecret = clientSecret});
        }
    }
}