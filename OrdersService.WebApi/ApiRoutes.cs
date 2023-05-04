using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi
{
    public static class ApiRoutes
    {
        public static class Authentication
        {
            public const string Login = "auth/login";

            public const string Register = "auth/registration";

            public const string RegisterAdmin = "auth/registration-admin";

            public const string RefreshToken = "auth/refresh-token";

            public const string Revoke = "auth/revoke";

            public const string RevokeAll = "auth/revoke-all";
        }

        public static class Order
        {
            public const string Create = "orders";

            public const string Update = "orders/";

            public const string Delete = "orders/";

            public const string Get = "orders/";
        }
    }
}
