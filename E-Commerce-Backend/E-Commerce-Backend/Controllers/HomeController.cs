using Microsoft.AspNetCore.Mvc;
using E_Commerce_Backend.Services;
using E_Commerce_Backend.Models;

namespace CodeMegaVNPay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public  PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("create-payment-url")]
        public IActionResult CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid payment information");
            }

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(new { paymentUrl = url });
        }

        [HttpGet("payment-callback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Ok(response);
        }
    }
}
