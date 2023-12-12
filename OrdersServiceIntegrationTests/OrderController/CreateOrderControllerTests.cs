using FluentAssertions;
using OrdersService.Application.Orders;
using OrdersService.Domain;
using OrdersService.WebApi;
using OrdersService.WebApi.Models.Exceptions;
using OrdersService.WebApi.Models.Order;
using System.Net;
using System.Net.Http.Json;

namespace OrdersService.IntegrationTests.OrderController
{
    public class CreateOrderControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Create_ShouldReturnValidationExceptionDto_WhenOrderLinesAreEmpty()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>(),
            };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, createOrderDto);
            var exceptionDto = await response.Content.ReadFromJsonAsync<ValidationExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exceptionDto.Errors.Should().NotBeNull();
            exceptionDto.Errors.Should().HaveCount(1);
            exceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.ValidationError);
        }

        [Fact]
        public async Task Create_ShouldReturnOrderVm_WhenOrderCreatedSuccessfully()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    }
                }
            };

            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, createOrderDto);
            var orderVm = await response.Content.ReadFromJsonAsync<OrderVm>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            orderVm.Status.Should().Be(OrderStatus.New);
            orderVm.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            orderVm.Lines.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_ShouldReturnAlreadyExistsExceptionDto_WhenOrderAlreadyExists()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    }
                }
            };
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, createOrderDto);

            //Act
            response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, createOrderDto);
            var alredyExistsExceptionDto = await response.Content.ReadFromJsonAsync<AlreadyExistsExceptionDto>();

            //Assert
            alredyExistsExceptionDto.Code.Should().Be(409);
            alredyExistsExceptionDto.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
            alredyExistsExceptionDto.Entity.Should().Be(nameof(Order));
            alredyExistsExceptionDto.Key.Should().Be(createOrderDto.Id.ToString());
            alredyExistsExceptionDto.Errors.Should().NotBeEmpty();
            alredyExistsExceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.AlreadyExists);
        }

        [Fact]
        public async Task Create_ShouldReturnUnauthorizedCode_WhenUserUnauthorized()
        {
            //Arrange
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                    },
                    new() {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    }
                }
            };
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, createOrderDto);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
