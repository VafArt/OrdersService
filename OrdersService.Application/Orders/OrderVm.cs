using AutoMapper;
using OrdersService.Application.Common.JsonConverters;
using OrdersService.Application.Common.Mappings;
using OrdersService.Domain;
using System.Text.Json.Serialization;

namespace OrdersService.Application.Orders
{
    public class OrderVm : IMapWith<Order>
    {
        public Guid Id { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("created")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("lines")]
        public ICollection<OrderLineVm> Lines { get; set; } = new List<OrderLineVm>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderVm>()
                .ForMember(orderVm => orderVm.Id,
                opt => opt.MapFrom(order => order.Id))
                .ForMember(orderVm => orderVm.Status,
                opt => opt.MapFrom(order => order.Status))
                .ForMember(orderVm => orderVm.DateCreated,
                opt => opt.MapFrom(order => order.DateCreated))
                .ForMember(orderVm => orderVm.Lines,
                opt => opt.MapFrom(order => order.Lines));
        }
    }
}
