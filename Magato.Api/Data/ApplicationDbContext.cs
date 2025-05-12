// <copyright file="ApplicationDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json;

using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Magato.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

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

    public DbSet<ProductImage> ProductImages
    {
        get; set;
    }

    public DbSet<Category> Categories
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

    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);

        modelBuilder.Entity<Product>()
.Property(p => p.Status)
.HasConversion<string>();

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<ProductInquiry>()
         .HasOne(i => i.Product)
         .WithMany(p => p.ProductInquiries)
         .HasForeignKey(i => i.ProductId)
         .OnDelete(DeleteBehavior.Cascade);

        /*  modelBuilder.Entity<Product>()
              .Property(p => p.ProductImages)
              .HasConversion(
                  v => JsonSerializer.Serialize(v.Select(i => i.ImageUrl).ToList(), (JsonSerializerOptions)null),
                  v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)?.Select(url => new ProductImage { ImageUrl = url }).ToList() ?? new List<ProductImage>()
              );*/

        modelBuilder.Entity<PageContent>()
        .HasMany(p => p.SocialMediaLinks)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);

        var stringListConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
            v => JsonSerializer.Deserialize<List<string>>(v, default(JsonSerializerOptions)) ?? new List<string>());

        modelBuilder.Entity<PageContent>()
       .Property(p => p.ImageUrls)
       .HasConversion(stringListConverter)
.Metadata.SetValueComparer(new ValueComparer<List<string>>(
    (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
    c => c == null ? 0 : c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
    c => c == null ? new List<string>() : c.ToList()));
    }
}
