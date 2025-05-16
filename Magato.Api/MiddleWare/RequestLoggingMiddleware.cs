// <copyright file="RequestLoggingMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.MiddleWare;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestLoggingMiddleware> logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        this.logger.LogInformation("Incoming Request: {method} {url}", request.Method, request.Path);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        await this.next(context);

        stopwatch.Stop();

        var statusCode = context.Response.StatusCode;
        this.logger.LogInformation(
            "Response: {method} {url} responded {statusCode} in {elapsed}ms",
            request.Method, request.Path, statusCode, stopwatch.ElapsedMilliseconds);
    }
}
