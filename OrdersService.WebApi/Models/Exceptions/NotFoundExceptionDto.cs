using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class NotFoundExceptionDto : ExceptionDto<ErrorDto>
    {
        [JsonPropertyName("entity")]
        public string Entity { get; set; } = string.Empty;

        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        public NotFoundExceptionDto()
        {
            Code = 404;
        }
    }
}
