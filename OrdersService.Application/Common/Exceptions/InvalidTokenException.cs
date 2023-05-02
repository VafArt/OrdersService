using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public string Token { get; set; }
        public InvalidTokenException(string token)
            :base("Invalid token")
        {
            Token = token;
        }
    }
}
