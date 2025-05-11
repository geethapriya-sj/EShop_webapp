using Eshop_Webapp.Areas.User.Controllers;
using Eshop_Webapp.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_Webapp.Controllers
{
    public class CartController : BaseController
    {
        private CartServiceClient _cartserviceClient;
        public CartController(CartServiceClient cartServiceClient)
        {
            _cartserviceClient = cartServiceClient;
        }
        public IActionResult Index()
        {
            if(CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var cart = _cartserviceClient.GetCartAsync(CurrentUser.UserId).Result;
                if (cart != null)
                {
                    return View(cart);
                }
                else
                {
                    return View(new CartModel());
                }
        }
    }
}
