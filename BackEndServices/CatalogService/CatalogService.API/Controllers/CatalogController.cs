using CatalogService.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        IProductAppService _productAppService;
        public CatalogController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productAppService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                var products = _productAppService.GetById(id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetByIds(int[] productIds)
        {
            var products = _productAppService.GetByIds(productIds);
            return Ok(products);
        }
    }
}
