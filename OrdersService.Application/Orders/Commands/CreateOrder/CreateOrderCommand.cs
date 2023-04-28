using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Domain;

namespace OrdersService.Application.Orders.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(Guid Id, ICollection<OrderLine> Lines) : ICommand<OrderVm>;
}
