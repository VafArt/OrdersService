using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Orders;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Application.Orders.Commands.DeleteOrder;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Application.Orders.Queries.GetOrder;
using OrdersService.Domain;
using OrdersService.WebApi.Examples.Requests;
using OrdersService.WebApi.Examples.Responses;
using OrdersService.WebApi.Models.Order;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace OrdersService.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;

        public OrderController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Creates order
        /// </summary>
        /// <param name="createOrderDto">Create order DTO</param>
        /// <returns>Returns OrderVm</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <responce code="409">If the order already exists</responce>
        /// <responce code="400">If validation error occurred</responce>
        [Authorize]
        [HttpPost(ApiRoutes.Order.Create)]
        [SwaggerResponseExample(201, typeof(OrderVmExample))]
        [SwaggerRequestExample(typeof(CreateOrderDto), typeof(CreateOrderDtoExample))]
        public async Task<ActionResult<OrderVm>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var createOrderCommand = _mapper.Map<CreateOrderCommand>(createOrderDto);
            var order = await Mediator.Send(createOrderCommand);
            return Created("/orders",order);
        }

        /// <summary>
        /// Updates order with specified id if status is New or AwaitingPayment
        /// </summary>
        /// <param name="updateOrderDto">Order DTO</param>
        /// <param name="id">Id of the order (guid)</param>
        /// <returns>Returns OrderVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If order is not found</response>
        /// <response code="403">If order status is not New or AwaitingPayment</response>
        /// <responce code="400">If validation error occurred</responce>
        [SwaggerResponseExample(200, typeof(OrderVmExample))]
        [SwaggerRequestExample(typeof(UpdateOrderDto), typeof(UpdateOrderDtoExample))]
        [Authorize]
        [HttpPut(ApiRoutes.Order.Update + "{id}")]
        public async Task<ActionResult<OrderVm>> Update(UpdateOrderDto updateOrderDto, Guid id)
        {
            updateOrderDto.Id = id;
            var updateOrderCommand = _mapper.Map<UpdateOrderCommand>(updateOrderDto);
            var order = await Mediator.Send(updateOrderCommand);
            return Ok(order);
        }

        /// <summary>
        /// Deletes order with specified id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Delete /e5a58b54-496f-489a-872d-e4d816c04f16
        /// </remarks>
        /// <param name="id">Id of the order (guid)</param>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If order is not found</response>
        /// <response code="403">If order status is SentForDelivery, Delivered or Completed</response>
        /// <responce code="400">If validation error occurred</responce>
        [Authorize]
        [HttpDelete(ApiRoutes.Order.Delete + "{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteOrderCommand = new DeleteOrderCommand(id);
            await Mediator.Send(deleteOrderCommand);
            return Ok();
        }

        /// <summary>
        /// Gets order with specified id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /e5a58b54-496f-489a-872d-e4d816c04f16
        /// </remarks>
        /// <param name="id">Id of the order (guid)</param>
        /// <returns>Returns OrderVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If order is not found</response>
        /// <responce code="400">If validation error occurred</responce>
        [Authorize]
        [HttpGet(ApiRoutes.Order.Get + "{id}")]
        [SwaggerResponseExample(200, typeof(OrderVmExample))]
        public async Task<ActionResult<OrderVm>> Get(Guid id)
        {
            var getOrderQuery = new GetOrderQuery(id);
            var order = await Mediator.Send(getOrderQuery);
            return Ok(order);
        }

    }
}
