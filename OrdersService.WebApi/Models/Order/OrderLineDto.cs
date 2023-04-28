using AutoMapper;
using OrdersService.Application.Common.Mappings;
using OrdersService.Domain;
using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Order
{
    public sealed class OrderLineDto : IMapWith<OrderLine>
    {
        [JsonPropertyName("id")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("qty")]
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderLineDto, OrderLine>();
        }
    }
}
