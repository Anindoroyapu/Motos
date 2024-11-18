using MotosAPI.Utils;
using NuGet.Protocol;
using System.Text;

namespace MotosAPI.Middleware
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            // If no endpoint is found and the response status code is 404, handle it
            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ApiResponseHandler.Error("Not Found").ToJson(), Encoding.UTF8);
            }
            else if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Not Found", Encoding.UTF8);
            }
        }
    }
}
