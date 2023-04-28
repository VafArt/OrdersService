using OrdersService.Application.Common.Abstractions.CQRS;

namespace OrdersService.Application.Orders.Commands.DeleteOrder
{
    public sealed record DeleteOrderCommand(Guid Id) : ICommand;
}
