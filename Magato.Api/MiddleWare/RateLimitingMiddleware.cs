// <copyright file="RateLimitingMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.MiddleWare;

using System.Collections.Concurrent;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate next;
    private static readonly ConcurrentDictionary<string, (DateTime resetTime, int count)> RequestLogs = new ();

    private const int LIMIT = 10;
    private static readonly TimeSpan PERIOD = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Admin ok
        if (context.User.Identity?.IsAuthenticated == true &&
            context.User.IsInRole("Admin"))
        {
            await this.next(context);
            return;
        }

        var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var now = DateTime.UtcNow;

        var entry = RequestLogs.GetOrAdd(key, _ => (now.Add(PERIOD), 0));

        if (entry.resetTime < now)
        {
            RequestLogs[key] = (now.Add(PERIOD), 1);
        }
        else
        {
            if (entry.count >= LIMIT)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            RequestLogs[key] = (entry.resetTime, entry.count + 1);
        }

        await this.next(context);
    }
}
