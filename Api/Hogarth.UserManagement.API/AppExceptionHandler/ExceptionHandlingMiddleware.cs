using System.Net;
using System.Text.Json;

namespace Hogarth.UserManagement.API.AppExceptionHandler
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Title = "Unexpected Error Occurred",
                ExceptionMessage = exception.Message,
                ExceptionDateTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                StackTrace = exception.StackTrace
            };

            string responseContent = JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions { WriteIndented = true });

            string logMessage = $@"
            Date & Time : {DateTime.UtcNow:dd/MM/yyyy hh:mm tt}
            Url : {context.Request.Path}
            HttpMethod: {context.Request.Method}
            Error Message: {exception.Message}
            -----------------------------------------------------------";

            _logger.LogError(logMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(responseContent);
        }
    }
}
