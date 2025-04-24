using CatalogService.Application.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Persistance.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly CatalogDBContext _context;
        public ProductRepository(CatalogDBContext context) 
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            Product product = _context.Products.Find(id);
            if(product != null)
            {
                _context.Products.Remove(product);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
