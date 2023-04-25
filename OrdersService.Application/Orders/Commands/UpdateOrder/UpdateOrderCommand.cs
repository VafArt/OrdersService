using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(Guid Id, OrderStatus Status,ICollection<OrderLine> Lines) : ICommand<Order>;
}
