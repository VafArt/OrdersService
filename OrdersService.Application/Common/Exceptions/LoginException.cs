using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Exceptions
{
    public class LoginException : Exception
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public LoginException(string login, string password)
            : base($"Wrong login or password")
        {
            Login = login;
            Password = password;
        }
    }
}
