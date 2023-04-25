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

namespace OrdersService.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrdersServiceDbContext _dbContext;

        public DeleteOrderCommandHandler(IOrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == request.Id, cancellationToken);
            if(order == null) throw new NotFoundException(nameof(Order), request.Id);

            if (order.Status == OrderStatus.SentForDelivery || order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Completed) throw new OrderDeleteIsForbiddenException(order.Status);

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
