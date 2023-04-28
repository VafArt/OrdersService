using MediatR;

namespace OrdersService.Application.Common.Abstractions.CQRS
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
