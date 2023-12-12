using OrdersService.WebApi.Models.Authentication;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Requests
{
    public class RegisterDtoExample : IExamplesProvider<RegisterDto>
    {
        public RegisterDto GetExamples()
        {
            return new RegisterDto
            {
                Username = "Arthur",
                Email = "arthur.vafin.post@gmail.com",
                Password = "Password123!",
            };
        }
    }
}
