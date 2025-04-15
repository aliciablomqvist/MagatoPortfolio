using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Magato.Api.Models;
using System.Text.Json;

namespace Magato.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Content
    public DbSet<PageContent> PageContents
    {
        get; set;
    }
    public DbSet<BlogPost> BlogPosts
    {
        get; set;
    }
    public DbSet<ContactMessage> ContactMessages
    {
        get; set;
    }

    // Product-related
    public DbSet<Product> Products
    {
        get; set;
    }
    public DbSet<ProductInquiry> ProductInquiries
    {
        get; set;
    }

    // Collection-related
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

    // Auth
    public DbSet<User> Users
    {
        get; set;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInquiry>()
         .HasOne(i => i.Product)
         .WithMany(p => p.ProductInquiries)
         .HasForeignKey(i => i.ProductId)
         .OnDelete(DeleteBehavior.Cascade);


        base.OnModelCreating(modelBuilder);

        // ValueConverter för MediaUrls
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
