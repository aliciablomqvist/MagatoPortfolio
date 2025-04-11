using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Magato.Api.Models;
using System.Text.Json;

namespace Magato.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Collection> Collections
    {
        get; set;
    }
    public DbSet<ColorOption> Colors
    {
        get; set;
    }
    public DbSet<Material> Materials
    {
        get; set;
    }
    public DbSet<Sketch> Sketches
    {
        get; set;
    }
    public DbSet<LookbookImage> LookbookImages
    {
        get; set;
    }
    public DbSet<ContactMessage> ContactMessages
    {
        get; set;
    }
    public DbSet<User> Users
    {
        get; set;
    }
    public DbSet<PageContent> PageContents
    {
        get; set;
    }

    public DbSet<BlogPost> BlogPosts
    {
        get; set;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ValueConverter for MediaUrls
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
        );

        modelBuilder.Entity<PageContent>(entity =>
        {
            entity.Property(e => e.MediaUrls)
                  .HasConversion(stringListConverter);
        });
    }
}
