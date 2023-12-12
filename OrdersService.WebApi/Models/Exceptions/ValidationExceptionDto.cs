using System.Text.Json.Serialization;

namespace OrdersService.WebApi.Models.Exceptions
{
    public class ValidationExceptionDto : ExceptionDto<ValidationErrorDto>
    {
        public ValidationExceptionDto()
        {
            Code = 400;
        }
    }
}
