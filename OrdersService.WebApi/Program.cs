using OrdersService.Application;
using OrdersService.Application.Common.Abstractions;
using OrdersService.Application.Common.JsonConverters;
using OrdersService.Application.Common.Mappings;
using OrdersService.Persistance;
using OrdersService.WebApi.Middleware;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IOrdersServiceDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(config=>
{
    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    config.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
    config.JsonSerializerOptions.AllowTrailingCommas = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var dbContext = serviceProvider.GetRequiredService<OrdersServiceDbContext>();
        DbInitializer.Initialize(dbContext);
    }
    catch (Exception ex)
    {

    }
}

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
