using FluentValidation.TestHelper;
using OrdersService.Application.Common.Validators;
using OrdersService.Application.Orders;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.UpdateOrderTests
{
    public class UpdateOrderCommandValidatorTests
    {
        private readonly UpdateOrderCommandValidator _validator;

        public UpdateOrderCommandValidatorTests()
        {
            _validator = new UpdateOrderCommandValidator();
        }

        [Fact]
        public async Task UpdateOrderCommandValidator_ShouldPass_WhenIdIsNotEmpty()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var orderLines = new List<OrderLine>()
            {
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 4,
                },
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                }
            };
            var order = new Order()
            {
                Id = orderId,
                Status = OrderStatus.New,
                DateCreated = DateTime.UtcNow,
                Lines = orderLines
            };
            var model = new UpdateOrderCommand(orderId, OrderStatus.Paid, orderLines);

            //Act
            var result = await _validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Id);
        }

        [Fact]
        public async Task UpdateOrderCommandValidator_ShouldPass_WhenLinesIsNotEmpty()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var orderLines = new List<OrderLine>()
            {
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 4,
                },
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 5,
                }
            };
            var order = new Order()
            {
                Id = orderId,
                Status = OrderStatus.New,
                DateCreated = DateTime.UtcNow,
                Lines = orderLines
            };
            var model = new UpdateOrderCommand(orderId, OrderStatus.Paid, orderLines);

            //Act
            var result = await _validator.TestValidateAsync(model);
            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Lines);
            _validator.ShouldHaveChildValidator(model => model.Lines, typeof(OrderLineValidator));
        }
    }
}
