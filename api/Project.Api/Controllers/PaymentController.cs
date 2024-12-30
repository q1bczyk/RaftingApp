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

        [HttpPost("PaymentInit")]
        public async Task<ActionResult> CreatePaymentIntent(BasePaymentDTO basePaymentDTO)
        {
            string clientSecret = await _stripeService.CreatePaymentIntent(basePaymentDTO);
            return Ok(new { ClientSecret = clientSecret});
        }
    }
}