using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(loginQuery => loginQuery.Username).NotEmpty();
            RuleFor(loginQuery => loginQuery.Password).NotEmpty();
        }
    }
}
