using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Domain;

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(Guid Id, OrderStatus Status,ICollection<OrderLine> Lines) : ICommand<OrderVm>;
}
