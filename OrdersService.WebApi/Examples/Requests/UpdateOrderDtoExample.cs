using OrdersService.Application.Orders;
using OrdersService.Domain;
using OrdersService.WebApi.Models.Order;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Requests
{
    public class UpdateOrderDtoExample : IExamplesProvider<UpdateOrderDto>
    {
        public UpdateOrderDto GetExamples()
        {
            return new UpdateOrderDto
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Paid,
                Lines = new List<OrderLineDto>
                {
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 13,
                    },
                    new OrderLineDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 2,
                    }
                }
            };
        }
    }
}
