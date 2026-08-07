using FluentValidation;
using SignHub.Application.Bases;
using SignHub.Application.Exceptions;
using System.Net;

namespace SignHub.WebAPI.CustomMiddlewares
{
    public class CustomExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new BaseResult<object>()
                {
                    Errors = ex.Errors.Select(x => new Error()
                    {
                        Code = x.PropertyName,
                        Message = x.ErrorMessage
                    }).ToList()
                };
                await context.Response.WriteAsJsonAsync(response);
            }

            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var statusCode = HttpStatusCode.InternalServerError;
                var message = "An unexpected error occurred.";

                if (ex is BaseException exception)
                {
                    message = exception.Message;
                    statusCode = exception.StatusCode;
                }
                else
                {
                    message = $"Internal Server Error: {ex.Message}";
                }

                context.Response.StatusCode = (int)statusCode;
                var response = BaseResult<object>.Failure(message);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
