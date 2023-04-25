using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KazanExpressBusiness.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost("{storeId}")]
        public async Task<ActionResult> Create(int storeId)
        {
            var createOrdersCommand = new CreateOrdersCommand()
            {
                StoreId = storeId,
            };
            await Mediator.Send(createOrdersCommand);
            return Ok();
        }

        [HttpGet("{storeId}")]
        public async Task<ActionResult<List<Order>>> GetAll(int storeId)
        {
            var getOrderListQuery = new GetOrderListQuery()
            {
                StoreId = storeId,
            };
            return Ok(await Mediator.Send(getOrderListQuery));
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<List<Order>>> Get(int orderId)
        {
            var getOrderDetailsQuery = new GetOrderDetailsQuery
            {
                OrderId = orderId,
            };
            return Ok(await Mediator.Send(getOrderDetailsQuery));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Order>>> Delete([FromQuery] int[] orderIds)
        {
            var deleteOrdersCommand = new DeleteOrdersCommand
            {
                OrderIds = orderIds
            };
            await Mediator.Send(deleteOrdersCommand);
            return Ok();
        }

    }
}
