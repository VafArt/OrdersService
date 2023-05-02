using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Exceptions
{
    public static class ExceptionCodes
    {
        public static string AlreadyExists = "alreadyExists";

        public static string InvalidToken = "invalidToken";

        public static string Login = "invalidLoginOrPassword";

        public static string NotFound = "notFound";

        public static string OrderChangeIsForbidden = "orderChangeIsForbidden";

        public static string OrderDeleteIsForbidden = "orderDeleteIsForbidden";

        public static string Registration = "invalidCredentials";

        public static string ValidationError = "validationError";
    }
}
