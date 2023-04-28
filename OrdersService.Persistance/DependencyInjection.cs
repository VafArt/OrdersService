using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Domain.Repositories;
using OrdersService.Persistance.Repositories;

namespace OrdersService.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration configuration)
        {
            var defaultConnection = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<OrdersServiceDbContext>(options =>
            {
                options.UseNpgsql(defaultConnection);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
