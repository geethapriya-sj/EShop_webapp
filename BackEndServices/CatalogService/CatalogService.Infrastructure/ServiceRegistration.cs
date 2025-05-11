using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstractions;
using CatalogService.Application.Services.Implementations;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration) {
            string connctionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<CatalogServiceDbContext>(options =>
            {
                options.UseSqlServer(connctionString);
            });

            //Repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            //Services
            services.AddScoped<IProductAppService, ProductAppService>();

            //AutoMapper
            services.AddAutoMapper(typeof(Application.Mappers.ProductMappers));
        }
    }
}
