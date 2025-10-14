using MangaReaderAPI.Exceptions;
using System.Net;
using System.Text.Json;

namespace MangaReaderAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                title = "An error occurred",
                status = (int) HttpStatusCode.InternalServerError,
                detail = "An unexpected error occurred"
            };

            switch (exception)
            {
                case EmailAlreadyExistsException:
                    response = new
                    {
                        title = "Email already exists",
                        status = (int) HttpStatusCode.Conflict,
                        detail = exception.Message
                    };
                    context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;

                case UsernameAlreadyExistsException:
                    response = new
                    {
                        title = "Username already exists",
                        status = (int) HttpStatusCode.Conflict,
                        detail = exception.Message
                    };
                    context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;

                case InvalidCredentialsException:
                    response = new
                    {
                        title = "Invalid credentials",
                        status = (int) HttpStatusCode.Unauthorized,
                        detail = exception.Message
                    };
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;

                default:
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
