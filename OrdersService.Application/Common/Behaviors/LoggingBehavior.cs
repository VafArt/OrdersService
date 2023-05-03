using MediatR;
using OrdersService.Application.Common.Services.CurrentUser;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUser;
        public LoggingBehavior(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId;

            Log.Information("Started orders Service Request: {Name} {@UserId} {@Request}",
                requestName, userId, request);

            var response = await next();

            Log.Information("Ended orders Service Request: {Name} {@UserId} {@Response}",
                requestName, userId, response);

            return response;
        }
    }
}
