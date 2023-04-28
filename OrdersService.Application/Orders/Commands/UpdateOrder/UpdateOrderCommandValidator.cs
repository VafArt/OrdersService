using FluentValidation;
using OrdersService.Application.Common.Validators;

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(createOrderCommand => createOrderCommand.Id).NotEmpty();
            RuleFor(createOrderCommand => createOrderCommand.Lines).NotEmpty();

            RuleForEach(createOrderCommand => createOrderCommand.Lines).NotEmpty().SetValidator(new OrderLineValidator());
        }
    }
}
