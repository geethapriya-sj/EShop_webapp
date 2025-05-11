using eShopFlix.Web.HttpClients;
using eShopFlix.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShopFlix.Web.Controllers
{
    public class PaymentController : BaseController
    {
        CartServiceClient _cartServiceClient;
        IConfiguration _configuration;
        public PaymentController(CartServiceClient cartServiceClient, IConfiguration configuration)
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

                return View(payment);
            }
            return RedirectToAction("Index","Cart");
        }
    }
}
