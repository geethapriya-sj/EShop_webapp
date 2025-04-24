using AuthService.Application.DTO.Mappers;
using CatalogService.Application.Mappers;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstraction;
using CatalogService.Application.Services.Implementation;
using CatalogService.Infrastructure.Persistance;
using CatalogService.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisteredServices(IServiceCollection services, IConfiguration configuration)
        {
            //database context
            services.AddDbContext<CatalogDBContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString("CatalogDb")));
            //repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            //services
            services.AddScoped<IProductAppService, ProductAppService>();

            //automapper
            services.AddAutoMapper(typeof(ProductMapper));
        }
    }
}
