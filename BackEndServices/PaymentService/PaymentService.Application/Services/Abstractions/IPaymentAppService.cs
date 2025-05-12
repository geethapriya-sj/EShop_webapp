using PaymentService.Application.DTOs;
using Razorpay.Api;

namespace PaymentService.Application.Services.Abstractions
{
    public interface IPaymentAppService
    {
        string CreateOrder(RazorPayOrderDTO order);
        Payment GetPaymentDetails(string paymentId);
        string VerifyPayment(PaymentConfirmDTO payment);
        bool SavePaymentDetails(PaymentDetailDTO model);
    }
}
