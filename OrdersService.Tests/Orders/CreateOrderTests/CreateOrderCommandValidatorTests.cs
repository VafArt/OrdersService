using FluentValidation.TestHelper;
using OrdersService.Application.Common.Validators;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.CreateOrderTests
{
    public class CreateOrderCommandValidatorTests
    {
        // - невозможно создать заказ без строк
        // - количество по строке заказа не может быть отрицательным
        // - количество по строке заказа не может быть 0

        private readonly CreateOrderCommandValidator _validator;

        public CreateOrderCommandValidatorTests()
        {
            _validator = new CreateOrderCommandValidator();
        }

        [Fact]
        public async Task CreateOrderCommandValidator_ShouldPass_WhenIdNotEmpty()
        {
            //Arrange
            var model = new CreateOrderCommand(Guid.NewGuid(), null);

            //Act
            var result = await _validator.TestValidateAsync(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Id);
            _validator.ShouldHaveChildValidator(model => model.Lines, typeof(OrderLineValidator));
        }

        [Fact]
        public async Task CreateOrderCommandValidator_ShouldPass_WhenLinesNotEmpty()
        {
            //Arrange
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
            var model = new CreateOrderCommand(Guid.NewGuid(), orderLines);

            //Act
            var result = await _validator.TestValidateAsync(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.Lines);
            _validator.ShouldHaveChildValidator(model => model.Lines, typeof(OrderLineValidator));
        }

    }
}
