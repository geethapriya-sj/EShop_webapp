using AuthService.Application.DTO.Mappers;
using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Application.Services.Implementation;
using AuthService.Infrastructure.Persistance;
using AuthService.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure
{
    public class ServiceRegistration
    {

        public static void RegisteredServices(IServiceCollection services, IConfiguration configuration) 
        {
            //database context
            services.AddDbContext<AuthServiceDBContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

            //repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //services
            services.AddScoped<IUserAppService, UserAppService>();

            //automapper
            services.AddAutoMapper(typeof(ApplicationMapper));
        }
    }
}
