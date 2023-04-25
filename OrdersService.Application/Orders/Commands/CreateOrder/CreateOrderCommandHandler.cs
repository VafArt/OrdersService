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

namespace OrdersService.Application.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
    {
        private readonly IOrdersServiceDbContext _dbContext;

        public CreateOrderCommandHandler(IOrdersServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(order => order.Id == request.Id, cancellationToken);
            if (order != null) throw new AlreadyExistsException(nameof(Order), request.Id);

            order = new Order()
            {
                Id = request.Id,
                Status = OrderStatus.New,
                DateCreated = DateTime.UtcNow,
                Lines = request.Lines,
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}
