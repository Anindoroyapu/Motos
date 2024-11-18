using MotosAPI.Utils;
using NuGet.Protocol;
using System.Text;

namespace MotosAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex}");

                // You can perform additional actions here, such as logging the error, sending an alert, etc.

                //--Create a log file

                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ApiResponseHandler.Error("Not Found").ToJson(), Encoding.UTF8);
            }
        }
    }
}
