using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstractions;
using CatalogService.Domain.Entities;
using Microsoft.Extensions.Configuration;


namespace CatalogService.Application.Services.Implementations
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly string _imageServer;
        private readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public ProductAppService(IProductRepository productRepository, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _productRepository = productRepository;
            _imageServer = _configuration["ImageServer"];
            _mapper = mapper;
        }

        public void Add(ProductDTO product)
        {
            var entity = _mapper.Map<Product>(product);
            _productRepository.Add(entity);
            _productRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
            _productRepository.SaveChanges();
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _productRepository.GetAll();
            if (products != null) {
                products = products.Select(p =>
                {
                    p.ImageUrl = _imageServer + p.ImageUrl;
                    return p;
                });
                return _mapper.Map<IEnumerable<ProductDTO>>(products);
            }
            return null;
        }

        public ProductDTO GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null) {
                product.ImageUrl = _imageServer + product.ImageUrl;
                return _mapper.Map<ProductDTO>(product);
            }
            return null;
        }

        public void Update(ProductDTO product)
        {
            var entity = _mapper.Map<Product>(product);
            _productRepository.Update(entity);
            _productRepository.SaveChanges();
        }

        public IEnumerable<ProductDTO> GetByIds(int[] ids)
        {
            var products = _productRepository.GetByIds(ids);
            foreach (var product in products)
            {
                if (product != null)
                {
                    product.ImageUrl = _imageServer + product.ImageUrl;
                }
            }
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
