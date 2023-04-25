using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrdersService.Domain
{
    public sealed class Order
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("lines")]
        public ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();
    }
}
