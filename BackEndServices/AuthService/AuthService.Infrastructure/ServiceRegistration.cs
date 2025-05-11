using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Application.Services.Implementations;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisteServices(IServiceCollection services, IConfiguration configuration)
        {
            //database
            services.AddDbContext<AuthServiceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //services
            services.AddScoped<IUserAppService, UserAppService>();

            //automapper
            services.AddAutoMapper(typeof(Application.Mappers.ApplicationMapper));
        }
    }
}
