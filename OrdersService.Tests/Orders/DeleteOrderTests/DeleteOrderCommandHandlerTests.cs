using FluentAssertions;
using Moq;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Orders.Commands.DeleteOrder;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.DeleteOrderTests
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenOrderNotFound()
        {
            //Arrange
            _orderRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            var id = Guid.NewGuid();
            var command = new DeleteOrderCommand(id);
            var handler = new DeleteOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowOrderDeleteIsForbiddenException_WhenStatusIsSentForDelivery()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
                Status = OrderStatus.SentForDelivery
            };
            _orderRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);
            var command = new DeleteOrderCommand(id);
            var handler = new DeleteOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<OrderDeleteIsForbiddenException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowOrderDeleteIsForbiddenException_WhenStatusIsDelivered()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
                Status = OrderStatus.Delivered
            };
            _orderRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);
            var command = new DeleteOrderCommand(id);
            var handler = new DeleteOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<OrderDeleteIsForbiddenException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowOrderDeleteIsForbiddenException_WhenStatusIsCompleted()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
                Status = OrderStatus.Completed
            };
            _orderRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);
            var command = new DeleteOrderCommand(id);
            var handler = new DeleteOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            //Act
            var result = async () => await handler.Handle(command, default);

            //Assert
            await result.Should().ThrowAsync<OrderDeleteIsForbiddenException>();
        }

        [Fact]
        public async Task Handle_ShouldCallRemoveOnRepository_WhenOrderExistsAndStatusIsNew()
        {
            //Arrange
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
                Status = OrderStatus.New
            };
            _orderRepositoryMock.Setup(x =>
            x.GetByIdAsync(It.Is<Guid>(i => i == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);

            var command = new DeleteOrderCommand(id);
            var handler = new DeleteOrderCommandHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object);

            //Act
            await handler.Handle(command, default);

            //Assert
            _orderRepositoryMock.Verify(x => x.Remove(order), Times.Once);
        }
    }
}
