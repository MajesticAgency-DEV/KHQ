using System.Globalization;

namespace KHQ.Middleware
{
    public class LocalizationHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Always call the next middleware first
            await _next(context);

            // After the controller/other middlewares run,
            // just set the Content-Language header.
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (!context.Response.HasStarted) // ensure response not committed
            {
                context.Response.Headers["Content-Language"] = culture;
            }
        }
    }

}
