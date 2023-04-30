using AutoMapper;
using FluentAssertions;
using Moq;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Orders;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.UpdateOrderTests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = MapperGenerator.CreateMapper();
        }

        [Theory]
        [InlineData(OrderStatus.New)]
        [InlineData(OrderStatus.AwaitingPayment)]
        public async Task Handler_ShouldPass_WhenOrderIsNewOrAwaitingPayment(OrderStatus status)
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
                Status = status,
                DateCreated = DateTime.UtcNow,
                Lines = orderLines
            };
            _orderRepositoryMock.Setup(x => x.GetByIdWithOrderLinesAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);

            var newStatus = OrderStatus.Paid;
            var newLines = new List<OrderLine>()
            {
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 6,
                },
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                }
            };
            var newLinesVm = _mapper.Map<List<OrderLineVm>>(newLines);
            var command = new UpdateOrderCommand(orderId, OrderStatus.Paid, newLines);
            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Status.Should().Be(newStatus);
            result.Lines.Should().BeEquivalentTo(newLinesVm);
            result.Id.Should().Be(orderId);
            result.DateCreated.Should().Be(order.DateCreated);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handler_ShouldThrowNotFoundException_WhenOrderIsNotFound()
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
            _orderRepositoryMock.Setup(x => x.GetByIdWithOrderLinesAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var newStatus = OrderStatus.Paid;
            var newLines = new List<OrderLine>()
            {
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 6,
                },
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                }
            };
            var newLinesVm = _mapper.Map<List<OrderLineVm>>(newLines);
            var command = new UpdateOrderCommand(orderId, OrderStatus.Paid, newLines);
            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Theory]
        [InlineData(OrderStatus.Paid)]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.SentForDelivery)]
        [InlineData(OrderStatus.Completed)]
        public async Task Handler_ShouldThrowOrderChangeIsForbiddenException_WhenOrderIsNotNewOrAwaitingPayment(OrderStatus status)
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
                Status = status,
                DateCreated = DateTime.UtcNow,
                Lines = orderLines
            };
            _orderRepositoryMock.Setup(x => x.GetByIdWithOrderLinesAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);

            var newStatus = OrderStatus.Paid;
            var newLines = new List<OrderLine>()
            {
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 6,
                },
                new OrderLine()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 3,
                }
            };
            var newLinesVm = _mapper.Map<List<OrderLineVm>>(newLines);
            var command = new UpdateOrderCommand(orderId, OrderStatus.Paid, newLines);
            var handler = new UpdateOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<OrderChangeIsForbiddenException>();
        }
    }
}
