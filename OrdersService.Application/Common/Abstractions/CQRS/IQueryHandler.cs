using MediatR;

namespace OrdersService.Application.Common.Abstractions.CQRS
{
    public interface IQueryHandler<TQuery,TResponse>
        : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
    }
}
