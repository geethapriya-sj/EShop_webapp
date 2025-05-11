using CatalogService.Application.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        CatalogServiceDbContext _dbContext;
        public ProductRepository(CatalogServiceDbContext db)
        {
            _dbContext = db;
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public void Delete(int id)
        {
            Product product = _dbContext.Products.Find(id);
            if (product != null) { 
                _dbContext.Remove(product);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public IEnumerable<Product> GetByIds(int[] ids)
        {
            return _dbContext.Products.Where(p => ids.Contains(p.ProductId));
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
