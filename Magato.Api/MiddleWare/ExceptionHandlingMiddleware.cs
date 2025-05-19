// <copyright file="ExceptionHandlingMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.MiddleWare;

using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
{
        try
{
            await this.next(context);
        }
        catch (Exception ex)
{
            this.logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/problem+json";

            var statusCode = ex switch
{
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var problem = new
{
                type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                title = GetTitleForStatus(statusCode),
                status = statusCode,
                detail = ex.Message, // Ta bort i prod.
            };

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }

    private static string GetTitleForStatus(int status)
{
        return status switch
{
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "An error occurred"
        };
    }
}
