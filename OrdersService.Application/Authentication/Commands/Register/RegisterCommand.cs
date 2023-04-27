using MediatR;
using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.Register
{
    public sealed record RegisterCommand(string Username, string Email, string Password) : ICommand;
}
