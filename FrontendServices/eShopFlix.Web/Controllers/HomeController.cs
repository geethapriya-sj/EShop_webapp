using eShopFlix.Web.HttpClients;
using eShopFlix.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eShopFlix.Web.Controllers
{
    public class HomeController : Controller
    {
        CatalogServiceClient _catalogServiceClient;
        
        public HomeController(CatalogServiceClient catalogServiceClient)
        {
            _catalogServiceClient = catalogServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogServiceClient.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
