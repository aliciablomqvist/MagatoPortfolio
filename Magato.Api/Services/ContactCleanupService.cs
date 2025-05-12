using Magato.Api.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Magato.Api.Services;
public class ContactCleanupService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromDays(1); // körs varje dag

    public ContactCleanupService(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _services.CreateScope();
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

            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }
}
