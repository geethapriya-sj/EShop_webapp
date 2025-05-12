using AutoMapper;
using Microsoft.Extensions.Configuration;
using PaymentService.Application.DTOs;
using PaymentService.Application.Repositories;
using PaymentService.Application.Services.Abstractions;
using PaymentService.Domain.Entities;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace PaymentService.Application.Services.Implementations
{
   
    public class PaymentAppService : IPaymentAppService
    {
        readonly RazorpayClient _client;
        IConfiguration _configuration;
        IPaymentRepository _paymentRepository;
        IMapper _mapper;

        public PaymentAppService(IConfiguration configuration, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _client = new RazorpayClient(_configuration["RazorPay:Key"], _configuration["RazorPay:Secret"]);
        }

        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string CreateOrder(RazorPayOrderDTO order)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", order.Amount); // amount in the smallest currency unit
            options.Add("receipt", order.Receipt);
            options.Add("currency", order.Currency);

            Order data = _client.Order.Create(options);
            return data["id"].ToString();
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            return _client.Payment.Fetch(paymentId);
        }

        public string VerifyPayment(PaymentConfirmDTO payment)
        {
            string payload = string.Format("{0}|{1}", payment.OrderId, payment.PaymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            bool status = actualSignature.Equals(payment.Signature);
            if (status)
            {
                Payment paymentDetails = GetPaymentDetails(payment.PaymentId);
                return paymentDetails["status"].ToString();
            }
            return "";
        }

        public bool SavePaymentDetails(PaymentDetailDTO model)
        {
            PaymentDetail payment = _mapper.Map<PaymentDetail>(model);
            return _paymentRepository.SavePaymentDetails(payment);
        }
    }
}
