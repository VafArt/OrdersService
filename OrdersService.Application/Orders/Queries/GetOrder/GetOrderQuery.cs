using OrdersService.Application.Common.Abstractions.CQRS;

namespace OrdersService.Application.Orders.Queries.GetOrder
{
    public sealed record GetOrderQuery(Guid Id) : IQuery<OrderVm>;
}
