using Eshop_Webapp.HttpClients;
using Eshop_Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Eshop_Webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatalogServiceClient _catalogServiceClient;

        public HomeController(CatalogServiceClient catalogServiceClient )
        {
            _catalogServiceClient = catalogServiceClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalogServiceClient.GetAllProductsAsync();
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
