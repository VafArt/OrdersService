using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class OrderDeleteIsForbiddenExceptionDto : ExceptionDto<ErrorDto>
    {
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("status")]
        public string OrderStatus { get; set; }

        public OrderDeleteIsForbiddenExceptionDto()
        {
            Code = 403;
        }
    }
}
