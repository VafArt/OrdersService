using OrdersService.Application.Authentication.Commands.Register;
using OrdersService.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Authentication
{
    public class RegisterDto : IMapWith<RegisterCommand>
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
