using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Repositories;
using PaymentService.Application.Services.Abstractions;
using PaymentService.Application.Services.Implementations;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Persistence.Repositories;

namespace PaymentService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentServiceContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Repositories
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            //Application Services
            services.AddScoped<IPaymentAppService, PaymentAppService>();

            //AutoMapper
            services.AddAutoMapper(typeof(Application.Mappers.PaymentMapper));
        }
    }
}
