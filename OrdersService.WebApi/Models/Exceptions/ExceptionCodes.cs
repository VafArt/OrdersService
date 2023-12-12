using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Exceptions
{
    public static class ExceptionCodes
    {
        public const string AlreadyExists = "alreadyExists";

        public const string InvalidToken = "invalidToken";

        public const string Login = "invalidLoginOrPassword";

        public const string NotFound = "notFound";

        public const string OrderChangeIsForbidden = "orderChangeIsForbidden";

        public const string OrderDeleteIsForbidden = "orderDeleteIsForbidden";

        public const string Registration = "invalidCredentials";

        public const string ValidationError = "validationError";
    }
}
