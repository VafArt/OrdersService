using OrdersService.WebApi.Models.Exceptions;
using OrdersService.WebApi.Models.Order;
using OrdersService.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Domain;
using System.Net.Http.Json;
using FluentAssertions;
using OrdersService.Application.Orders;

namespace OrdersService.IntegrationTests.OrderController
{
    public class UpdateOrderControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Update_ShouldReturnValidationExceptionDto_WhenOrderLinesAreEmpty()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };
            await CreateOrderAsync(createOrderDto);
            var updateOrderDto = new UpdateOrderDto
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>()
            };


            //Act
            var response = await TestClient. PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);
            var exceptionDto = await response.Content.ReadFromJsonAsync<ValidationExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exceptionDto.Errors.Should().HaveCount(1);
            exceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.ValidationError);
        }

        [Fact]
        public async Task Update_ShouldReturnOrderChangeIsForbiddenExceptionDto_WhenOrderStatusIsNotNewOrAwaitingPayment()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };
            await CreateOrderAsync(createOrderDto);
            var updateOrderDto = new UpdateOrderDto
            {
                Id = createOrderDto.Id,
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };
            await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);
            updateOrderDto.Status = OrderStatus.Completed;

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);
            var exceptionDto = await response.Content.ReadFromJsonAsync<OrderChangeIsForbiddenExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            exceptionDto.Errors.Should().HaveCount(1);
            exceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.OrderChangeIsForbidden);
        }

        [Fact]
        public async Task Update_ShouldReturnOrderVm_WhenOrderUpdatedSuccessfully()
        {
            //Arrange
            await AuthenticateAsync();
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };
            await CreateOrderAsync(createOrderDto);
            var updateOrderDto = new UpdateOrderDto
            {
                Id = createOrderDto.Id,
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);
            var orderVm = await response.Content.ReadFromJsonAsync<OrderVm>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            orderVm.Status.Should().Be(OrderStatus.Paid);
            orderVm.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            orderVm.Id.Should().Be(updateOrderDto.Id.ToString());
            orderVm.Lines.Should().BeEquivalentTo(updateOrderDto.Lines);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFoundExceptionDto_WhenOrderNotFound()
        {
            //Arrange
            await AuthenticateAsync();
            var updateOrderDto = new UpdateOrderDto
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);
            var notFoundExceptionDto = await response.Content.ReadFromJsonAsync<NotFoundExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            notFoundExceptionDto.Entity.Should().Be(nameof(Order));
            notFoundExceptionDto.Code.Should().Be(404);
            notFoundExceptionDto.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            notFoundExceptionDto.Key.Should().Be(updateOrderDto.Id.ToString());
            notFoundExceptionDto.Errors.Should().NotBeEmpty();
            notFoundExceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.NotFound);
        }

        [Fact]
        public async Task Update_ShouldReturnUnauthorized_WhenUserUnauthorized()
        {
            //Arrange
            var createOrderDto = new CreateOrderDto
            {
                Id = Guid.NewGuid(),
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };
            await CreateOrderAsync(createOrderDto);
            var updateOrderDto = new UpdateOrderDto
            {
                Id = createOrderDto.Id,
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 12,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                    },
                }
            };

            //Act
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + updateOrderDto.Id.ToString(), updateOrderDto);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
