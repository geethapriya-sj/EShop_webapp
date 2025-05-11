using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Mappers
{
    public class ProductMappers: Profile
    {
        public ProductMappers() {
            CreateMap<Product, ProductDTO>();
        }
    }
}
