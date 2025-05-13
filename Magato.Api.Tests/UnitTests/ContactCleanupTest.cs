using FluentAssertions;

using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Tests.UnitTests.Services;
public class ContactCleanupTests
{
    [Fact]
    public async Task CleanupOldMessagesAsync_RemovesMessagesOlderThan90Days()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);
        context.ContactMessages.AddRange(
            new ContactMessage
            {
                Name = "Gammal",
                Email = "gammal@example.com",
                Message = "Detta är gammalt",
                CreatedAt = DateTime.UtcNow.AddDays(-91)
            },
            new ContactMessage
            {
                Name = "Ny",
                Email = "ny@example.com",
                Message = "Detta ska inte tas bort",
                CreatedAt = DateTime.UtcNow
            }
        );

        await context.SaveChangesAsync();
        var cutoff = DateTime.UtcNow.AddDays(-90);
        var oldMessages = await context.ContactMessages
            .Where(m => m.CreatedAt < cutoff)
            .ToListAsync();

        context.ContactMessages.RemoveRange(oldMessages);
        await context.SaveChangesAsync();
        var remaining = await context.ContactMessages.ToListAsync();
        remaining.Should().HaveCount(1);
        remaining[0].Name.Should().Be("Ny");
    }
}
