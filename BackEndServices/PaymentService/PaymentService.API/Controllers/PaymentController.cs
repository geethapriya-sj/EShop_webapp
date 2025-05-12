using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs;
using PaymentService.Application.Services.Abstractions;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IPaymentAppService _paymentService;
        public PaymentController(IPaymentAppService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult CreateOrder(RazorPayOrderDTO order)
        {
            string orderId = _paymentService.CreateOrder(order);
            return Ok(orderId);
        }

        [HttpPost]
        public IActionResult VerifyPayment(PaymentConfirmDTO payment)
        {
            string status = _paymentService.VerifyPayment(payment);
            return Ok(status);
        }

        [HttpPost]
        public IActionResult SavePaymentDetails(PaymentDetailDTO payment)
        {
            bool status = _paymentService.SavePaymentDetails(payment);
            return Ok(status);
        }
    }
}
