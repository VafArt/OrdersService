using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Commands.DeleteOrder
{
    public sealed record DeleteOrderCommand(Guid Id) : ICommand;
}
