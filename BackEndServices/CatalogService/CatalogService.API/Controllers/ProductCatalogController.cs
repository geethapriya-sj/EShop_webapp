using CatalogService.Application.DTO;
using CatalogService.Application.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalogController : ControllerBase
    {
        private readonly IProductAppService _productAppService;
        public ProductCatalogController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productAppService.GetAllProducts();
            if (products != null)
            {
                return Ok(products);
            }
            return NotFound("No products found");
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productAppService.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound("Product not found");
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }
            _productAppService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            if (product == null || product.ProductId != id)
            {
                return BadRequest("Product ID mismatch");
            }
            var existingProduct = _productAppService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }
            _productAppService.UpdateProduct(product);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productAppService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }
            _productAppService.DeleteProduct(id);
            return NoContent();
        }


        [HttpGet("search")]
        public IActionResult SearchProducts([FromQuery] string searchTerm)
        {
            var products = _productAppService.GetAllProducts();
            if (products != null)
            {
                var filteredProducts = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                return Ok(filteredProducts);
            }
            return NotFound("No products found");
        }
    }
}
