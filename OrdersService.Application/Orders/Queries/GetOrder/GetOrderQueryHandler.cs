using AutoMapper;
using OrdersService.Application.Common.Abstractions.CQRS;
using OrdersService.Application.Common.Exceptions;
using OrdersService.Domain;
using OrdersService.Domain.Repositories;

namespace OrdersService.Application.Orders.Queries.GetOrder
{
    public sealed class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderVm>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderVm> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdWithOrderLinesAsync(request.Id, cancellationToken);
            if (order == null) throw new NotFoundException(nameof(Order), request.Id.ToString());
            order.Lines = order.Lines.Reverse().ToList();
            return _mapper.Map<OrderVm>(order);
        }
    }
}
