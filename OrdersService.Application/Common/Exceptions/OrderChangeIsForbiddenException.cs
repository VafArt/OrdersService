using OrdersService.Domain;

namespace OrdersService.Application.Common.Exceptions
{
    public class OrderChangeIsForbiddenException : Exception
    {
        public string OrderId { get; set; }

        public string OrderStatus { get; set; }

        public OrderChangeIsForbiddenException(string orderId, OrderStatus status)
            :base($"Can not change order with status {status}")
        {
            OrderId = orderId;
            OrderStatus = status.ToString();
        }
    }
}
