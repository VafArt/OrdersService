using AutoMapper;
using FluentAssertions;
using Moq;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Orders;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Tests.Orders.CreateOrderTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        private readonly IMapper _mapper;

        public CreateOrderCommandHandlerTests()
        {
            _unitOfWorkMock = new();
            _orderRepositoryMock = new();
            _mapper = MapperGenerator.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnOrderVm_WhenOrderDoesNotExists()
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
            var orderCommand = new CreateOrderCommand(Guid.NewGuid(), orderLines);
            var orderCommandHandler = new CreateOrderCommandHandler(
                _unitOfWorkMock.Object,
                _orderRepositoryMock.Object,
                _mapper);

            //Act
            var result = await orderCommandHandler.Handle(orderCommand, default);

            //Assert
            result.Id.Should().Be(orderCommand.Id);
            result.Status.Should().Be(OrderStatus.New);
            result.Lines.Should().BeEquivalentTo(orderLines.Select(orderline => new OrderLineVm
            {
                ProductId = orderline.ProductId,
                Quantity = orderline.Quantity
            }).ToList());
            result.DateCreated.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(500));
        }

        [Fact]
        public async Task Handle_ShouldReturnAlreadyExistsException_WhenOrderExists()
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
            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.New,
                DateCreated = DateTime.Now,
                Lines = orderLines,
            };

            var orderCommand = new CreateOrderCommand(orderId, orderLines);
            var orderCommandHandler = new CreateOrderCommandHandler(
                _unitOfWorkMock.Object,
                _orderRepositoryMock.Object,
                _mapper);

            _orderRepositoryMock.Setup(x => x.GetByIdAsync(orderCommand.Id, default))
                .ReturnsAsync(order);

            //Act
            var result = async () => await orderCommandHandler.Handle(orderCommand, default);

            //Assert
            await result.Should().ThrowAsync<AlreadyExistsException>();
        }

        [Fact]
        public async Task Handle_ShouldCallAddOnRepository_WhenOrderDoesNotExists()
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
            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.New,
                DateCreated = DateTime.Now,
                Lines = orderLines,
            };

            var orderCommand = new CreateOrderCommand(orderId, orderLines);
            var orderCommandHandler = new CreateOrderCommandHandler(
                _unitOfWorkMock.Object,
                _orderRepositoryMock.Object,
                _mapper);

            //Act
            var result = await orderCommandHandler.Handle(orderCommand, default);

            //Assert
            _orderRepositoryMock.Verify(x =>
            x.Add(It.Is<Order>(o => o.Id == orderId)), Times.Once);
            _unitOfWorkMock.Verify(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
