using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class RegistrationExceptionDto : ExceptionDto<ErrorDto>
    {
        public RegistrationExceptionDto()
        {
            Code = 400;
        }
    }
}
