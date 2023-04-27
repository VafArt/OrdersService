using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RegisterAdmin
{
    public sealed record RegisterAdminCommand(string Username, string Email, string Password) : ICommand;
}
