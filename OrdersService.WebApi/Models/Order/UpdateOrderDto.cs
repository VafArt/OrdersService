using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Common.Mappings;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.WebApi.Models.Order
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        [FromQuery]
        public Guid? Id { get; set; }

        [FromBody]
        public OrderStatus? Status { get; set; }

        [FromBody]
        public ICollection<OrderLine>? Lines { get; set; } = new List<OrderLine>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(orderCommand => orderCommand.Id,
                opt => opt.MapFrom(orderDto => orderDto.Id))
                .ForMember(orderCommand => orderCommand.Status,
                opt => opt.MapFrom(orderDto => orderDto.Status))
                .ForMember(orderCommand => orderCommand.Lines,
                opt => opt.MapFrom(orderDto => orderDto.Lines));
        }
    }
}
