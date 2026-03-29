namespace Ticket.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var reqTime = DateTime.Now;
            await _next(context);

            var duration = DateTime.Now - reqTime;
            Console.WriteLine($"Path: {context.Request.Path} - Xử lý trong: {duration.TotalMilliseconds}ms");
        }
    }
}
