using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.Revoke
{
    public class RevokeCommandValidator : AbstractValidator<RevokeCommand>
    {
        public RevokeCommandValidator()
        {
            RuleFor(revokeCommand => revokeCommand.Username).NotEmpty();
        }
    }
}
