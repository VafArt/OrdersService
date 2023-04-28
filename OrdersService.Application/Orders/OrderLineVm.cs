using AutoMapper;
using OrdersService.Application.Common.Mappings;
using OrdersService.Domain;
using System.Text.Json.Serialization;

namespace OrdersService.Application.Orders
{
    public class OrderLineVm : IMapWith<OrderLine>
    {
        [JsonPropertyName("id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("qty")]
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderLine, OrderLineVm>()
                .ForMember(orderLineVm => orderLineVm.ProductId,
                opt => opt.MapFrom(orderLine => orderLine.ProductId))
                .ForMember(orderLineVm => orderLineVm.Quantity,
                opt => opt.MapFrom(orderLine => orderLine.Quantity));
        }
    }
}
