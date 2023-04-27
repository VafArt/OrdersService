using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RevokeAll
{
    public sealed record RevokeAllCommand() : ICommand;
}
