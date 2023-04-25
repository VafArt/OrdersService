using FluentValidation;
using FluentValidation.Validators;
using OrdersService.Application.Common.Validators;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(createOrderCommand => createOrderCommand.Id).NotEmpty();
            RuleFor(createOrderCommand => createOrderCommand.Lines).NotEmpty();
            RuleForEach(createOrderCommand => createOrderCommand.Lines).NotEmpty().SetValidator(new OrderLineValidator());
        }
    }
}
