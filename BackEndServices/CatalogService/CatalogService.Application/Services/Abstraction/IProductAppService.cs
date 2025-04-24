using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Services.Abstraction
{
    public  interface IProductAppService
    {
        IEnumerable<DTO.ProductDTO> GetAllProducts();
        DTO.ProductDTO GetProductById(int id);
        void AddProduct(DTO.ProductDTO product);
        void UpdateProduct(DTO.ProductDTO product);
        void DeleteProduct(int id);
    }
}
