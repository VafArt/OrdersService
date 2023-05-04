using OrdersService.Domain;
using OrdersService.WebApi.Models.Exceptions;
using OrdersService.WebApi.Models.Order;
using OrdersService.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http.Json;
using OrdersService.Application.Orders;

namespace OrdersService.IntegrationTests.OrderController
{
    public class GetOrderControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_ShouldReturnOrderVm_WhenOrderExists()
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
            var response = await TestClient.GetAsync(ApiRoutes.Order.Get + createOrderDto.Id.ToString());
            var orderVm = await response.Content.ReadFromJsonAsync<OrderVm>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            orderVm.Status.Should().Be(OrderStatus.New);
            orderVm.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            orderVm.Id.Should().Be(createOrderDto.Id.ToString());
            orderVm.Lines.Should().BeEquivalentTo(createOrderDto.Lines);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFoundExceptionDto_WhenOrderNotFound()
        {
            //Arrange
            await AuthenticateAsync();
            var id = Guid.NewGuid();

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Order.Get + id.ToString());
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
        public async Task Get_ShouldReturnUnauthorized_WhenUserUnauthorized()
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
            var response = await TestClient.GetAsync(ApiRoutes.Order.Get + createOrderDto.Id.ToString());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
