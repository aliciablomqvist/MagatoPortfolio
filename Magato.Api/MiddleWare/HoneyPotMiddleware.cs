public class HoneypotMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HoneypotMiddleware> _logger;

    public HoneypotMiddleware(RequestDelegate next, ILogger<HoneypotMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method is "POST" or "PUT")
        {
            context.Request.EnableBuffering(); // Tillåt att läsa bodyn flera gånger

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (body.Contains("\"honeypot\"") &&
                !body.Contains("\"honeypot\":\"\""))
            {
                _logger.LogWarning("Honeypot triggered from IP: {IP}", context.Connection.RemoteIpAddress);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Spam detected.");
                return;
            }
        }

        await _next(context);
    }
}
