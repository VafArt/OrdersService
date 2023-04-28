using FluentValidation;

namespace OrdersService.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(deleteOrderCommand => deleteOrderCommand.Id).NotEmpty();
        }
    }
}
