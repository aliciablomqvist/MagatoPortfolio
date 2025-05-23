// <copyright file="InputValidationMiddleware.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.MiddleWare;

using System.Text.Json;

public class InputValidationMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<InputValidationMiddleware> logger;

    public InputValidationMiddleware(RequestDelegate next, ILogger<InputValidationMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method is "POST" or "PUT" or "PATCH" &&
            context.Request.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            try
            {
                JsonDocument.Parse(body);
            }
            catch (JsonException ex)
            {
                this.logger.LogWarning("Invalid JSON:{Message}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid JSON format.");
                return;
            }
        }

        await this.next(context);
    }
}
