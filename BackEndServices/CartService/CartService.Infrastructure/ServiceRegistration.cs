using CartService.Application.Repositories;
using CartService.Infrastructure.Persistence.Repositories;
using CartService.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CartService.Application.Services.Abstractions;
using CartService.Application.Services.Implementations;

namespace CartService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //DBContext
            string ConnectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<CartServiceDbContext>(options => options.UseSqlServer(ConnectionString));

            //Repositories
            services.AddScoped<ICartRepository, CartRepository>();

            //Services
            services.AddScoped<ICartAppService, CartAppService>();

            //AutoMapper
            services.AddAutoMapper(typeof(Application.Mappers.CartMapper));
        }
    }
}
