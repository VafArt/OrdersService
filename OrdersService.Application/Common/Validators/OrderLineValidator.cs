using FluentValidation;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.Common.Validators
{
    public class OrderLineValidator : AbstractValidator<OrderLine>
    {
        public OrderLineValidator()
        {
            RuleFor(orderLine => orderLine.ProductId).NotEmpty();
            RuleFor(orderLine => orderLine.Quantity).GreaterThan(0).WithMessage("Количество должно быть больше 0!");
        }
    }
}
