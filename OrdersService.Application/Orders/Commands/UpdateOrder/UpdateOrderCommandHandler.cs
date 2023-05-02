using AutoMapper;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Application.Orders.Commands.UpdateOrder
{
    internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, OrderVm>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithOrderLinesAsync(request.Id, cancellationToken);
            if (order == null) throw new NotFoundException(nameof(Order), request.Id.ToString());

            if (!(order.Status == OrderStatus.New || order.Status == OrderStatus.AwaitingPayment)) throw new OrderChangeIsForbiddenException(request.Id.ToString(), order.Status);

            order.Status = request.Status;
            order.Lines = request.Lines;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderVm>(order);
        }
    }
}
