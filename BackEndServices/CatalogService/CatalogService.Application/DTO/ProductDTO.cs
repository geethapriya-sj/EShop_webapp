using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string description { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
