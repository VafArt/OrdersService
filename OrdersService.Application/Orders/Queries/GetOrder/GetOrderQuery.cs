using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Queries.GetOrder
{
    public sealed record GetOrderQuery(Guid Id) : IQuery<Order>;
}
