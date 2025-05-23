// <copyright file="ContactCleanupService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class ContactCleanupService : BackgroundService
{
    private readonly IServiceProvider services;
    private readonly TimeSpan cleanupInterval = TimeSpan.FromDays(1); // körs varje dag

    public ContactCleanupService(IServiceProvider services)
    {
        this.services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = this.services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var cutoffDate = DateTime.UtcNow.AddDays(-90);
            var oldMessages = await db.ContactMessages
                .Where(m => m.CreatedAt < cutoffDate)
                .ToListAsync();

            if (oldMessages.Any())
            {
                db.ContactMessages.RemoveRange(oldMessages);
                await db.SaveChangesAsync();
            }

            await Task.Delay(this.cleanupInterval, stoppingToken);
        }
    }
}
