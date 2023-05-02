using OrdersService.Application.Orders;
using OrdersService.Domain;
using OrdersService.WebApi.Models.Order;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Requests
{
    public class CreateOrderDtoExample : IExamplesProvider<CreateOrderDto>
    {
        public CreateOrderDto GetExamples()
        {
            return new CreateOrderDto
            {
                Id = Guid.NewGuid(),
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
