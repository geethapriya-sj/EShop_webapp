using eShopFlix.Web.Helpers;
using eShopFlix.Web.HttpClients;
using eShopFlix.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlix.Web.Controllers
{
    public class PaymentController : BaseController
    {
        readonly CartServiceClient _cartServiceClient;
        readonly IConfiguration _configuration;
        readonly PaymentServiceClient _paymentServiceClient;
        public PaymentController(CartServiceClient cartServiceClient,PaymentServiceClient paymentServiceClient, IConfiguration configuration)
        {
            _cartServiceClient = cartServiceClient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            CartModel cartModel = _cartServiceClient.GetUserCartAsync(CurrentUser.UserId).Result;
            if (cartModel != null) { 
                PaymentModel payment = new PaymentModel();
                payment.Cart = cartModel;
                payment.Currency = "INR";
                payment.Description = string.Join(",", cartModel.CartItems.Select(x => x.Name));
                payment.GrandTotal = cartModel.GrandTotal;
                RazorPayOrderModel razorpayOrder = new RazorPayOrderModel
                {
                     Amount = Convert.ToInt32(payment.GrandTotal * 100),
                     Currency = payment.Currency,
                     Receipt = payment.Receipt
                };
                payment.OrderId = _paymentServiceClient.CreateOrderAsync(razorpayOrder).Result;
                return View(payment);
            }
            return RedirectToAction("Index","Cart");
        }
        public async Task<IActionResult> Status(IFormCollection form)
        {
            if (!string.IsNullOrEmpty(form["rzp_paymentid"]))
            {
                string paymentId = form["rzp_paymentid"];
                string orderId = form["rzp_orderid"];
                string signature = form["rzp_signature"];
                string transactionId = form["Receipt"];
                string currency = form["Currency"];

                PaymentConfirmModel payment = new PaymentConfirmModel
                {
                    PaymentId = paymentId,
                    OrderId = orderId,
                    Signature = signature
                };

                string status = await _paymentServiceClient.VerifyPaymentAsync(payment);
                if (status == "captured" || status == "completed")
                {
                    CartModel cart = await _cartServiceClient.GetUserCartAsync(CurrentUser.UserId);
                    TransactionModel model = new TransactionModel();

                    model.CartId = cart.Id;
                    model.Total = cart.Total;
                    model.Tax = cart.Tax;
                    model.GrandTotal = cart.GrandTotal;
                    model.CreatedDate = DateTime.Now;

                    model.Status = status;
                    model.TransactionId = transactionId;
                    model.Currency = currency;
                    model.Email = CurrentUser.Email;
                    model.Id = paymentId;
                    model.UserId = CurrentUser.UserId;

                    bool result = await _paymentServiceClient.SavePaymentDetailsAsync(model);
                    if (result)
                    {
                        TempData.Set("Receipt", model);
                        return RedirectToAction("Receipt");
                    }
                }
            }
            ViewBag.Message = "Due to some technical issues we are not able to receive order details. We will contact you soon..";
            return View();
        }

        public IActionResult Receipt()
        {
            TransactionModel model = TempData.Get<TransactionModel>("Receipt");
            return View(model);
        }
    }
}
