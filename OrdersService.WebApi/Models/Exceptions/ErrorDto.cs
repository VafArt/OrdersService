namespace OrdersService.WebApi.Models.Exceptions
{
    public class ErrorDto
    {
        public string Code { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
