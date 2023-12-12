using OrdersService.Domain;

namespace OrdersService.Application.Common.Exceptions
{
    public class OrderDeleteIsForbiddenException : Exception
    {
        public string OrderId { get; set; }

        public string OrderStatus { get; set; }

        public OrderDeleteIsForbiddenException(OrderStatus status, string orderId)
            : base($"Запрещено удалять заказ с статусом {status}")
        {
            OrderId = orderId;
            OrderStatus = status.ToString();
        }
    }
}
