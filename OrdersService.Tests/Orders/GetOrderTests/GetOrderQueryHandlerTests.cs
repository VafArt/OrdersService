using AutoMapper;
using FluentAssertions;
using Moq;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Orders.Queries.GetOrder;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Tests.Orders.GetOrderTests
{
    public class GetOrderQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;

        public GetOrderQueryHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapper = MapperGenerator.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnOrderVm_WhenOrderExists()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var query = new GetOrderQuery(orderId);
            var handler = new GetOrderQueryHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);
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
            var order = new Order
            {
                Id = orderId,
                Status = OrderStatus.New,
                DateCreated = DateTime.Now,
                Lines = orderLines
            };
            _orderRepositoryMock.Setup(x => x.GetByIdWithOrderLinesAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Status.Should().BeOneOf(new[] {OrderStatus.New, OrderStatus.AwaitingPayment, OrderStatus.Paid, OrderStatus.SentForDelivery, OrderStatus.Delivered, OrderStatus.Completed} );
            result.Lines.Should().NotBeEmpty();
            result.Id.Should().Be(orderId);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenOrderDoesNotExists()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var query = new GetOrderQuery(orderId);
            var handler = new GetOrderQueryHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = async () => await handler.Handle(query, default);

            //Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
