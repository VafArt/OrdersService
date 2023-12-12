using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class LoginExceptionDto : ExceptionDto<ErrorDto>
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Pasword { get; set; } = string.Empty;

        public LoginExceptionDto()
        {
            Code = 409;
        }
    }
}
