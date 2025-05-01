using System.Collections.Concurrent;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, (DateTime resetTime, int count)> _requestLogs = new();

    private const int LIMIT = 10;
    private static readonly TimeSpan PERIOD = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Admin ok
        if (context.User.Identity?.IsAuthenticated == true &&
            context.User.IsInRole("Admin"))
        {
            await _next(context);
            return;
        }

        var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var now = DateTime.UtcNow;

        var entry = _requestLogs.GetOrAdd(key, _ => (now.Add(PERIOD), 0));

        if (entry.resetTime < now)
        {
            _requestLogs[key] = (now.Add(PERIOD), 1);
        }
        else
        {
            if (entry.count >= LIMIT)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            _requestLogs[key] = (entry.resetTime, entry.count + 1);
        }

        await _next(context);
    }
}
