// <copyright file="Program.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

using Magato.Api;
using Magato.Api.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

builder.Services.AddMagatoServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

if (!app.Environment.IsDevelopment() && !app.Environment.IsEnvironment("Testing"))
{
    app.UseRateLimiting();
    app.UseGlobalExceptionHandling();
    app.UseInputValidation();
}

app.UseRequestLogging();
app.UseHoneypot();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();

public partial class Program
{
}
