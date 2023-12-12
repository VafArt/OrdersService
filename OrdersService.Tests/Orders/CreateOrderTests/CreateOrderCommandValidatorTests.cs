using FluentValidation.TestHelper;
using OrdersService.Application.Common.Validators;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Domain;

namespace OrdersService.Tests.Orders.CreateOrderTests
{
    public class CreateOrderCommandValidatorTests
    {
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
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 4,
                },
                new()
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
