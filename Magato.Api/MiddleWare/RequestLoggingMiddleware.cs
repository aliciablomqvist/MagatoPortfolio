
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        _logger.LogInformation("Incoming Request: {method} {url}", request.Method, request.Path);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var statusCode = context.Response.StatusCode;
        _logger.LogInformation("Response: {method} {url} responded {statusCode} in {elapsed}ms",
            request.Method, request.Path, statusCode, stopwatch.ElapsedMilliseconds);
    }
}
