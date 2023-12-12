using OrdersService.Application.Authentication.Commands.RefreshToken;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection.Metadata.Ecma335;

namespace OrdersService.WebApi.Examples.Responses
{
    public class RefreshTokenVmExample : IExamplesProvider<RefreshTokenVm>
    {
        public RefreshTokenVm GetExamples()
        {
            return new RefreshTokenVm
            {
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQXJ0aHVyIiwianRpIjoiNzQyNmMyODEtYjBkYy00ZmQwLThjYjEtMzhkMWI2M2IzNTNhIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY4MzAzMTQyNSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE5MyIsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo3MTkzIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE5MyJdfQ.m0ZN_tUNJ_KOEQ0c7NzxUIT6SYzSIm5l-I7-daJgDKY",
                RefreshToken = "luzPOhskhQCX20Raskp01f0ICFyrtYhiBF4wywjmMpY="
            };
        }
    }
}
