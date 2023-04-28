using FluentValidation;
using OrdersService.Application.Common.Validators;

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
