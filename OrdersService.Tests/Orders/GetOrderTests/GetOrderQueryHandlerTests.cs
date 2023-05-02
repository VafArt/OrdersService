using AutoMapper;
using FluentAssertions;
using Moq;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Application.Orders.Queries.GetOrder;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Handle_ShouldReturnOrderVm_WhenOrderExists()
        {
            //Arrange
            var orderId = Guid.NewGuid();
            var query = new GetOrderQuery(orderId);
            var handler = new GetOrderQueryHandler(
                _orderRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper);

            //Act
            var result = await handler.Handle(query, default);
            //Assert
            result.Status.Should().BeOneOf(new[] {OrderStatus.New, OrderStatus.AwaitingPayment, OrderStatus.Paid, OrderStatus.SentForDelivery, OrderStatus.Delivered, OrderStatus.Completed} );
            result.Lines.Should().NotBeEmpty();
            result.Id.Should().Be(orderId);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

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
