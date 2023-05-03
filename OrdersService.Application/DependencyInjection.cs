using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.Common.Behaviors;
using OrdersService.Application.Common.Services.CurrentUser;
using OrdersService.Application.Common.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OrdersService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddTransient<ITokenService, TokenService>();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
