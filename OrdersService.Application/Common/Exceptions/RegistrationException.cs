using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Exceptions
{
    public class RegistrationException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }

        public RegistrationException(IEnumerable<IdentityError> errors)
            : base("Invalid user credentials! Check credentials and try again!")
        {
            Errors = errors;
        }
    }
}
