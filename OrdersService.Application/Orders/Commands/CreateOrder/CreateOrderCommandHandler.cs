using AutoMapper;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Application.Orders.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order != null) throw new AlreadyExistsException(nameof(Order), request.Id.ToString());

            order = new Order()
            {
                Id = request.Id,
                Status = OrderStatus.New,
                DateCreated = DateTime.UtcNow,
                Lines = request.Lines,
            };

            _orderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderVm>(order);
        }
    }
}
