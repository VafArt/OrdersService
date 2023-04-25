using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Domain
{
    public enum OrderStatus
    {
        New,
        AwaitingPayment,
        Paid,
        SentForDelivery,
        Delivered,
        Completed
    }
}
