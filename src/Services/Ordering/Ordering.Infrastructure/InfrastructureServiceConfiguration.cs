using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Infrastructure.Mail;
using Ordering.Application.Models;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            services.Configure<EmailSettings>(options => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            return services;
        }
    }
}
