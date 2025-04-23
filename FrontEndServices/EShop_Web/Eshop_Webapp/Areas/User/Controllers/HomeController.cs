using Microsoft.AspNetCore.Mvc;

namespace Eshop_Webapp.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
