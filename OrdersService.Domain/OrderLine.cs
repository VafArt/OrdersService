using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrdersService.Domain
{
    public sealed class OrderLine
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonPropertyName("id")]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Guid OrderId { get; set; }

        [JsonPropertyName("qty")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public Order Order { get; set; } = new Order();
    }
}
