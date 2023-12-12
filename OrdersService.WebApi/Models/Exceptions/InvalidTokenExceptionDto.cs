using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class InvalidTokenExceptionDto : ExceptionDto<ErrorDto>
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        public InvalidTokenExceptionDto()
        {
            Code = 400;
        }
    }
}
