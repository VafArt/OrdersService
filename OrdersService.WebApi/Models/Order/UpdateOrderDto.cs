using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Application.Common.JsonConverters;
using OrdersService.Application.Common.Mappings;
using OrdersService.Application.Orders.Commands.UpdateOrder;
using OrdersService.Domain;
using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Order
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        [FromQuery]
        [JsonConverter(typeof(JsonStringGuidConverter))]
        public Guid? Id { get; set; }

        [FromBody]
        public OrderStatus? Status { get; set; }

        [FromBody]
        public ICollection<OrderLineDto>? Lines { get; set; } = new List<OrderLineDto>();

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
