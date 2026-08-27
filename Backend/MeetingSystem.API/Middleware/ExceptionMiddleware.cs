using MeetHub.Application.Exceptions;
using MeetHub.Domain.Exceptions;
namespace MeetHub.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var statusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BusinessRuleException => StatusCodes.Status400BadRequest,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = statusCode;
                await System.Text.Json.JsonSerializer.SerializeAsync(context.Response.Body, new { error = ex.Message });
                await context.Response.Body.FlushAsync();
            }
        }
    }
}
