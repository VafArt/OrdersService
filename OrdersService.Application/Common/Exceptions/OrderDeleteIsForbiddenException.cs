using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
