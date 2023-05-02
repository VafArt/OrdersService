using FluentValidation;
using OrdersService.Application.Common.Exceptions;
using OrdersService.WebApi.Models.Exceptions;
using System.Net;
using System.Text.Json;

namespace OrdersService.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    var validationExceptionDto = new ValidationExceptionDto
                    {
                        Date = DateTime.UtcNow,
                        Errors = validationException.Errors.Select(error => new ValidationErrorDto { Message = error.ErrorMessage, Code = ExceptionCodes.ValidationError, PropertyName = error.PropertyName}).ToList(),
                    };
                    result = JsonSerializer.Serialize(validationExceptionDto);
                    break;
                case NotFoundException notFound:
                    code = HttpStatusCode.NotFound;
                    var notFoundDto = new NotFoundExceptionDto
                    {
                        Entity = notFound.Entity,
                        Key = notFound.Key,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto> 
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.NotFound,
                                Message = notFound.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(notFoundDto);
                    break;
                case AlreadyExistsException alreadyExists:
                    code = HttpStatusCode.Conflict;
                    var alreadyExistsDto = new AlreadyExistsExceptionDto
                    {
                        Entity = alreadyExists.Entity,
                        Key = alreadyExists.Key,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto>
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.AlreadyExists,
                                Message = alreadyExists.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(alreadyExistsDto);
                    break;
                case InvalidTokenException invalidToken:
                    code = HttpStatusCode.BadRequest;
                    var invalidTokenDto = new InvalidTokenExceptionDto
                    {
                        Token = invalidToken.Token,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto> 
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.InvalidToken,
                                Message = invalidToken.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(invalidTokenDto);
                    break;
                case LoginException loginException:
                    code = HttpStatusCode.Conflict;
                    var loginExceptionDto = new LoginExceptionDto
                    {
                        Username = loginException.Login,
                        Pasword = loginException.Password,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto>
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.Login,
                                Message = loginException.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(loginExceptionDto);
                    break;
                case OrderChangeIsForbiddenException orderChangeIsForbiddenException:
                    code = HttpStatusCode.Forbidden;
                    var orderChangeIsForbiddenDto = new OrderChangeIsForbiddenExceptionDto
                    {
                        OrderId = orderChangeIsForbiddenException.OrderId,
                        OrderStatus = orderChangeIsForbiddenException.OrderStatus,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto>
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.OrderChangeIsForbidden,
                                Message = orderChangeIsForbiddenException.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(orderChangeIsForbiddenDto);
                    break;
                case OrderDeleteIsForbiddenException orderDeleteIsForbiddenException:
                    code = HttpStatusCode.Forbidden;
                    var orderDeleteIsForbiddenDto = new OrderDeleteIsForbiddenExceptionDto
                    {
                        OrderId = orderDeleteIsForbiddenException.OrderId,
                        OrderStatus = orderDeleteIsForbiddenException.OrderStatus,
                        Date = DateTime.UtcNow,
                        Errors = new List<ErrorDto>
                        {
                            new ErrorDto
                            {
                                Code = ExceptionCodes.OrderDeleteIsForbidden,
                                Message = orderDeleteIsForbiddenException.Message,
                            }
                        }
                    };
                    result = JsonSerializer.Serialize(orderDeleteIsForbiddenDto);
                    break;
                case RegistrationException registrationException:
                    code = HttpStatusCode.BadRequest;
                    var registrationExceptionDto = new RegistrationExceptionDto
                    {
                        Date = DateTime.UtcNow,
                        Errors = registrationException.Errors.Select(error => new ErrorDto { Code = ExceptionCodes.Registration, Message = error.Description }).ToList(),
                    };
                    result = JsonSerializer.Serialize(registrationExceptionDto);
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if(result == string.Empty)
                result = JsonSerializer.Serialize(new { error = exception.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
