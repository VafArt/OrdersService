using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.Authentication;
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
            var identityConnection = configuration["ConnectionStrings:AuthenticationConnection"];
            services.AddDbContext<OrdersServiceDbContext>(options =>
            {
                options.UseNpgsql(defaultConnection);
            });
            services.AddDbContext<OrdersServiceIdentityDbContext>(options =>
            {
                options.UseNpgsql(identityConnection);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OrdersServiceIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IOrdersServiceDbContext>(provider =>
            provider.GetService<OrdersServiceDbContext>());

            return services;
        }
    }
}
