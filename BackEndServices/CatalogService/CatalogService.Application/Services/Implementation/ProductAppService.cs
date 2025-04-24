using AutoMapper;
using CatalogService.Application.DTO;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstraction;
using CatalogService.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Application.Services.Implementation
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly string imageServer;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _productRepository = productRepository;
            imageServer = _configuration["ImageServer"]; // Fixed by ensuring the required namespace is included
        }
        public IEnumerable<DTO.ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            if (products != null)
            {
                products = products.Select(p =>
                {
                    p.ImageUrl = imageServer + p.ImageUrl;
                    return p;
                });
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            return null;
        }
        public DTO.ProductDTO GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                product.ImageUrl = imageServer + product.ImageUrl;
                return _mapper.Map<DTO.ProductDTO>(product);
            }
            return null;
        }
        public void AddProduct(ProductDTO product)
        {
            var productentity = _mapper.Map<Product>(product);
            _productRepository.AddProduct(productentity);
            _productRepository.SaveChanges();
        }
        public void UpdateProduct(DTO.ProductDTO product)
        {
            var productentity = _mapper.Map<Product>(product);
            if (productentity != null)
            {
                _productRepository.UpdateProduct(productentity);
                _productRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                _productRepository.DeleteProduct(id);
                _productRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
