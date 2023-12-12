using FluentValidation.Validators;
using FluentValidation;
using OrdersService.Application.Authentication.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Authentication.Commands.RegisterAdmin
{
    public class RegisterAdminCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterAdminCommandValidator()
        {
            RuleFor(registerCommand => registerCommand.Username).NotEmpty();
            RuleFor(registerCommand => registerCommand.Password).NotEmpty();
            RuleFor(registerCommand => registerCommand.Email).NotEmpty().EmailAddress(EmailValidationMode.Net4xRegex);
        }
    }
}
