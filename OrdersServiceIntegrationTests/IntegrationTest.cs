using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OrdersService.Application.Authentication.Commands.Register;
using OrdersService.Application.Authentication.Queries.Login;
using OrdersService.Persistance;
using OrdersService.WebApi;
using OrdersService.WebApi.Controllers;
using OrdersService.WebApi.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace OrdersService.IntegrationTests
{
    public abstract class IntegrationTest : IAsyncLifetime
    {
        protected readonly HttpClient TestClient;

        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("ordersServiceDb")
            .WithUsername("postgres")
            .WithPassword("Asakura1")
            .WithEnvironment("TZ", "Europe/Moscow")
            .Build();

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        var descriptor = services
                        .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<OrdersServiceDbContext>));
                        if (descriptor is not null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<OrdersServiceDbContext>(options =>
                        {
                            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=ordersServiceDb;User Id=postgres;Password=Asakura1;");
                        });
                    });
                    
                });
            TestClient = appFactory.CreateClient();
        }

        protected async Task<bool> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Order.Create, orderDto);
            if (response.StatusCode == HttpStatusCode.Created)
                return true;
            return false;
        }

        protected async Task<bool> UpdateOrderAsync(UpdateOrderDto orderDto)
        {
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Order.Update + orderDto.Id.ToString(), orderDto);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var registrationResponse = await TestClient.PostAsJsonAsync(ApiRoutes.Authentication.Register, new RegisterCommand
            {
                Username = "test",
                Email = "test@test.com",
                Password = "Test123!"
            });

            var loginResponse = await TestClient.PostAsJsonAsync(ApiRoutes.Authentication.Login, new LoginQuery
            {
                Username = "test",
                Password = "Test123!"
            });
            var content = await loginResponse.Content.ReadAsStringAsync();
            var loginResponseVm = await loginResponse.Content.ReadFromJsonAsync<LoginVm>();

            return loginResponseVm.AccessToken;
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
