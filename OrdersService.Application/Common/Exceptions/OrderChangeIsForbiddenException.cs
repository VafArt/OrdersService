using OrdersService.Domain;

namespace OrdersService.Application.Common.Exceptions
{
    public class OrderChangeIsForbiddenException : Exception
    {
        public OrderChangeIsForbiddenException(OrderStatus status)
            :base($"Запрещено изменять заказ с статусом {status}")
        {

        }
    }
}
