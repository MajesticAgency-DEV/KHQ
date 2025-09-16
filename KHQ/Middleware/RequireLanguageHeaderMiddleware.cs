namespace KHQ.Middleware
{
    public class RequireLanguageHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public RequireLanguageHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Accept-Language"))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Missing Accept-Language header");
                return;
            }

            await _next(context);
        }
    }

}
