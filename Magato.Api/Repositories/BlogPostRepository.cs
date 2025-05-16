// <copyright file="BlogPostRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext context;

    public BlogPostRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public BlogPost? Get(int id) => this.context.BlogPosts.Find(id);

    public BlogPost? GetBySlug(string slug)
  => this.context.BlogPosts.FirstOrDefault(p => p.Slug == slug);

    public IEnumerable<BlogPost> GetByTag(string tag)
        => this.context.BlogPosts.Where(p => p.Tags != null && p.Tags.Contains(tag)).ToList();

    public IEnumerable<BlogPost> GetAll() => this.context.BlogPosts.ToList();

    public void Add(BlogPost post)
    {
        this.context.BlogPosts.Add(post);
        this.context.SaveChanges();
    }

    public void Update(BlogPost post)
    {
        var existing = this.context.BlogPosts.Find(post.Id);
        if (existing == null)
        {
            return;
        }

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.Author = post.Author;
        existing.PublishedAt = post.PublishedAt;
        existing.Slug = post.Slug;
        existing.Tags = post.Tags;
        existing.ImageUrls = post.ImageUrls;
        this.context.SaveChanges();
    }

    public void Delete(int id)
    {
        var post = this.context.BlogPosts.Find(id);
        if (post == null)
        {
            return;
        }

        this.context.BlogPosts.Remove(post);
        this.context.SaveChanges();
    }
}
