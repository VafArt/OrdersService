using FluentValidation.TestHelper;
using OrdersService.Application.Orders.Commands.DeleteOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.DeleteOrderTests
{
    public class DeleteOrderCommandValidatorTests
    {
        private readonly DeleteOrderCommandValidator _validator;

        public DeleteOrderCommandValidatorTests()
        {
            _validator = new DeleteOrderCommandValidator();
        }

        [Fact]
        public async Task DeleteOrderCommandValidator_ShouldPass_WhenIdIsNotEmpty()
        {
            //Arrange
            var id = Guid.NewGuid();
            var command = new DeleteOrderCommand(id);
            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Id);
        }

        [Fact]
        public async Task DeleteOrderCommandValidator_ShouldFail_WhenIdIsEmpty()
        {
            //Arrange
            var id = Guid.Empty;
            var command = new DeleteOrderCommand(id);
            //Act
            var result = await _validator.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Id);
        }
    }
}
