using OrdersService.Domain;

namespace OrdersService.Application.Common.Exceptions
{
    public class OrderDeleteIsForbiddenException : Exception
    {
        public OrderDeleteIsForbiddenException(OrderStatus status)
            : base($"Запрещено удалять заказ с статусом {status}")
        {

        }
    }
}
