using OrdersService.Application.Orders;
using OrdersService.Domain;
using OrdersService.WebApi.Models.Exceptions;
using OrdersService.WebApi.Models.Order;
using OrdersService.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace OrdersService.IntegrationTests.OrderController
{
    public class DeleteOrderControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Delete_ShouldReturnOrderDeleteIsForbiddenExceptionDto_WhenOrderStatusIsSentForDeliveryOrDeliveredOrCompleted()
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
                Status = OrderStatus.Completed,
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
            var a = await UpdateOrderAsync(updateOrderDto);

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Order.Delete + updateOrderDto.Id.ToString());
            var exceptionDto = await response.Content.ReadFromJsonAsync<OrderDeleteIsForbiddenExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            exceptionDto.Errors.Should().HaveCount(1);
            exceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.OrderDeleteIsForbidden);
        }

        [Fact]
        public async Task Delete_ShouldReturnStatusCodeOk_WhenOrderDeletedSuccessfully()
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

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Order.Delete + createOrderDto.Id.ToString());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundExceptionDto_WhenOrderNotFound()
        {
            //Arrange
            await AuthenticateAsync();
            var id = Guid.NewGuid();

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Order.Delete + id.ToString());
            var notFoundExceptionDto = await response.Content.ReadFromJsonAsync<NotFoundExceptionDto>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            notFoundExceptionDto.Entity.Should().Be(nameof(Order));
            notFoundExceptionDto.Code.Should().Be(404);
            notFoundExceptionDto.Date.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            notFoundExceptionDto.Key.Should().Be(id.ToString());
            notFoundExceptionDto.Errors.Should().NotBeEmpty();
            notFoundExceptionDto.Errors.First().Code.Should().Be(ExceptionCodes.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnUnauthorized_WhenUserUnauthorized()
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

            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Order.Delete + createOrderDto.Id.ToString());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
