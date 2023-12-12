using OrdersService.WebApi.Models.Authentication;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Requests
{
    public class LoginDtoExample : IExamplesProvider<LoginDto>
    {
        public LoginDto GetExamples()
        {
            return new LoginDto
            {
                Username = "Arthur",
                Password = "Password123!",
            };
        }
    }
}
