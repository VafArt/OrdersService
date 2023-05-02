using AutoMapper;
using OrdersService.Application.Common.JsonConverters;
using OrdersService.Application.Common.Mappings;
using OrdersService.Application.Orders.Commands.CreateOrder;
using OrdersService.Domain;
using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Order
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(JsonStringGuidConverter))]
        public Guid? Id { get; set; }

        [JsonPropertyName("lines")]
        public List<OrderLineDto>? Lines { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
                .ForMember(orderCommand => orderCommand.Id,
                opt => opt.MapFrom(orderDto => orderDto.Id))
                .ForMember(orderCommand => orderCommand.Lines,
                opt => opt.MapFrom(orderDto => orderDto.Lines));
        }
    }
}