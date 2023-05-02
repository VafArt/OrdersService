using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class OrderChangeIsForbiddenExceptionDto : ExceptionDto<ErrorDto>
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("status")]
        public string OrderStatus { get; set; }

        public OrderChangeIsForbiddenExceptionDto()
        {
            Code = 403;
        }
    }
}
