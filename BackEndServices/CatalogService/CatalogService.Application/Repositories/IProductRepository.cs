﻿using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        int SaveChanges();
    }
}
