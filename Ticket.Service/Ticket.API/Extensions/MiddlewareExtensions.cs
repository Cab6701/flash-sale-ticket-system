using Ticket.API.Middlewares;

namespace Ticket.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
