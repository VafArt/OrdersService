using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Application.Orders.Commands.DeleteOrder;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Application.Orders.Queries.GetOrder;
using OrdersService.Domain;
using OrdersService.WebApi.Controllers;
using OrdersService.WebApi.Models.Order;

namespace KazanExpressBusiness.WebApi.Controllers
{
    [Route("orders")]
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderDto>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var order = await Mediator.Send(createOrderCommand);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateOrderDto>> Update(UpdateOrderDto updateOrderDto, Guid id)
        {
            updateOrderDto.Id = id;
            var updateOrderCommand = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
            var order = await Mediator.Send(updateOrderCommand);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Order>>> Delete(Guid id)
        {
            var deleteOrderCommand = new DeleteOrderCommand(id);
            await Mediator.Send(deleteOrderCommand);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            var getOrderQuery = new GetOrderQuery(id);
            var order = await Mediator.Send(getOrderQuery);
            return Ok(order);
        }

    }
}