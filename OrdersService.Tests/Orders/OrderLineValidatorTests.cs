using FluentValidation.TestHelper;
using OrdersService.Application.Common.Validators;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders
{
    public class OrderLineValidatorTests
    {
        private readonly OrderLineValidator _validator;

        public OrderLineValidatorTests()
        {
            _validator = new OrderLineValidator();
        }

        [Fact]
        public async Task OrderLineValidator_ShouldPass_WhenProductIdNotEmpty()
        {
            //Arrange
            var model = new OrderLine
            {
                ProductId = Guid.NewGuid(),
            };

            //Act
            var result = await _validator.TestValidateAsync(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(model => model.ProductId);
        }

        [Fact]
        public async Task OrderLineValidator_ShouldFail_WhenProductIdEmpty()
        {
            //Arrange
            var model = new OrderLine
            {
                ProductId = Guid.Empty,
            };

            //Act
            var result = await _validator.TestValidateAsync(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.ProductId);
        }

        [Fact]
        public async Task OrderLineValidator_ShouldFail_WhenQuantityLowerThanOne()
        {
            //Arrange
            var model = new OrderLine
            {
                Quantity = -1,
            };

            //Act
            var result = await _validator.TestValidateAsync(model);

            //Assert
            result.ShouldHaveValidationErrorFor(model => model.Quantity);
        }
    }
}
