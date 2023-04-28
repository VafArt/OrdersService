using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);
            if(order == null) throw new NotFoundException(nameof(Order), request.Id);

            if (order.Status == OrderStatus.SentForDelivery || order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Completed) throw new OrderDeleteIsForbiddenException(order.Status);

            _orderRepository.Remove(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
