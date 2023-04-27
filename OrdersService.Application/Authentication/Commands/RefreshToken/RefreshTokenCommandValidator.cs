using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(refreshTokenCommand=>refreshTokenCommand.AccessToken).NotEmpty();
            RuleFor(refreshTokenCommand => refreshTokenCommand.RefreshToken).NotEmpty();
        }
    }
}
