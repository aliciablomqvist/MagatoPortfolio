// <copyright file="HoneyPotMiddleware.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.MiddleWare;
public class HoneypotMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<HoneypotMiddleware> logger;

    public HoneypotMiddleware(RequestDelegate next, ILogger<HoneypotMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
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
                this.logger.LogWarning("Honeypot triggered from IP:{IP}", context.Connection.RemoteIpAddress);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Spam detected.");
                return;
            }
        }

        await this.next(context);
    }
}
