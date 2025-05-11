using PaymentService.Application.DTOs;
using PaymentService.Application.Services.Abstractions;
using Razorpay.Api;

namespace PaymentService.Application.Services.Implementations
{
    public class PaymentAppService : IPaymentAppService
    {
        public string CreateOrder(RazorPayOrderDTO order)
        {
            throw new NotImplementedException();
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            throw new NotImplementedException();
        }

        public bool SavePaymentDetails(PaymentDetailDTO model)
        {
            throw new NotImplementedException();
        }

        public string VerifyPayment(PaymentConfirmDTO payment)
        {
            throw new NotImplementedException();
        }
    }
}
