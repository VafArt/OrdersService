using Microsoft.EntityFrameworkCore;
using OrdersService.Application.Common.Abstractions;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Queries.GetOrder
{
    public sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, Order>
    {
        private readonly IOrdersServiceDbContext _dbContext;

        public GetOrderQueryHandler(IOrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(order => order.Lines)
                .FirstOrDefaultAsync(order => order.Id == request.Id, cancellationToken);
            if (order == null) throw new NotFoundException(nameof(Order), request.Id);
            return order;
        }
    }
}
