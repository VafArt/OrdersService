using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenVm
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
