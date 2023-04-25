using FluentValidation;
using OrdersService.Application.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(createOrderCommand => createOrderCommand.Id).NotEmpty();
            RuleFor(createOrderCommand => createOrderCommand.Lines).NotEmpty();
            RuleFor(createOrderCommand => createOrderCommand.Status).NotEmpty();

            RuleForEach(createOrderCommand => createOrderCommand.Lines).NotEmpty().SetValidator(new OrderLineValidator());
        }
    }
}
