using MediatR;

namespace OrdersService.Application.Common.Abstractions.CQRS
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}
