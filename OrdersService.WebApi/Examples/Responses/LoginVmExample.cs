using OrdersService.Application.Authentication.Queries.Login;
using Swashbuckle.AspNetCore.Filters;

namespace OrdersService.WebApi.Examples.Responses
{
    public class LoginVmExample : IExamplesProvider<LoginVm>
    {
        private readonly IConfiguration _configuration;

        public LoginVmExample(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoginVm GetExamples()
        {
            return new LoginVm
            {
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQXJ0aHVyIiwianRpIjoiNTJjMjA0ZWQtYWRmNC00ZTJhLTgzMTUtZDYxMzU1ZTEzYjY1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY4MzAzMDQyMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE5MyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcxOTMifQ.QMd6PvE57jo8FT_0JfJ89voD1QZTzAkSRmccuh4Id94",
                RefreshToken = "vDBxNT3ekP5s6SBpj/UYlMw5Zjg9h3dBfyh9d5kFzfk=",
                Expiration = DateTime.UtcNow + TimeSpan.FromMinutes(double.Parse(_configuration["JWT:TokenValidityInMinutes"])),
            };
        }
    }
}
