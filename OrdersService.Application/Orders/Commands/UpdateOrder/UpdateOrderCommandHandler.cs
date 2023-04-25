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

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrdersServiceDbContext _dbContext;

        public UpdateOrderCommandHandler(IOrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(order => order.Lines)
                .FirstOrDefaultAsync(order => order.Id == request.Id, cancellationToken);
            if (order == null) throw new NotFoundException(nameof(Order), request.Id);

            if (!(order.Status == OrderStatus.New || order.Status == OrderStatus.AwaitingPayment)) throw new OrderChangeIsForbiddenException(order.Status);

            order.Status = request.Status;
            order.Lines = request.Lines;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}
