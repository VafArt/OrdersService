using OrdersService.Application.Orders;
using OrdersService.Domain;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Responses
{
    public class OrderVmExample : IExamplesProvider<OrderVm>
    {
        public OrderVm GetExamples()
        {
            return new OrderVm
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Paid,
                DateCreated = DateTime.UtcNow,
                Lines = new List<OrderLineVm>
                {
                    new OrderLineVm
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                    },
                    new OrderLineVm
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 13,
                    },
                    new OrderLineVm
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    }
                }
            };
        }
    }
}