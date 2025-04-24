using Eshop_Webapp.Areas.User.Controllers;
using Eshop_Webapp.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_Webapp.Controllers
{
    public class CartController : BaseController
    {
        private CartServiceClient _cartserviceClient;
        public CartController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
