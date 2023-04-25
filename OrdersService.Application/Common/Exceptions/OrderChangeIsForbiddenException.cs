using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
