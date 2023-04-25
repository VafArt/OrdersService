using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddScoped<IOrdersServiceDbContext>(provider =>
            provider.GetService<OrdersServiceDbContext>());

            return services;
        }
    }
}
