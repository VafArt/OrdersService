using Destructurama.Attributed;
using MediatR;
using OrdersService.Application.Common.Abstractions.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Queries.Login
{
    public sealed record LoginQuery() : IQuery<LoginVm>
    {
        public string Username { get; set; }

        [NotLogged]
        public string Password { get; set; }
    }
}
