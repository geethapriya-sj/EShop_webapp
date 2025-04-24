using CartService.Application.Mappers;
using CartService.Application.Repositories;
using CartService.Application.Service.Abstractions;
using CartService.Application.Service.Implementation;
using CartService.Infrastructure.Persistance;
using CartService.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void registerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CartMapper));
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartAppService, CartAppService>();
            services.AddDbContext<CartServiceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CartDB")));
        }
    }
}
