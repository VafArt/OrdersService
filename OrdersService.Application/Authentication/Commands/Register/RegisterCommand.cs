using Destructurama.Attributed;
using MediatR;
using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.Register
{
    public sealed record RegisterCommand() : ICommand
    {
        public string Username { get; set; }

        public string Email { get; set; }

        [NotLogged]
        public string Password { get; set; }
    }
}
