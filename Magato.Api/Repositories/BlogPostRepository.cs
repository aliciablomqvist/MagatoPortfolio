using Magato.Api.Data;
using Magato.Api.Models;


using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;
public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext _context;

    public BlogPostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public BlogPost? Get(int id) => _context.BlogPosts.Find(id);

    public BlogPost? GetBySlug(string slug)
  => _context.BlogPosts.FirstOrDefault(p => p.Slug == slug);

    public IEnumerable<BlogPost> GetByTag(string tag)
        => _context.BlogPosts.Where(p => p.Tags != null && p.Tags.Contains(tag)).ToList();

    public IEnumerable<BlogPost> GetAll() => _context.BlogPosts.ToList();

    public void Add(BlogPost post)
    {
        _context.BlogPosts.Add(post);
        _context.SaveChanges();
    }

    public void Update(BlogPost post)
    {
        var existing = _context.BlogPosts.Find(post.Id);
        if (existing == null)
            return;

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.Author = post.Author;
        existing.PublishedAt = post.PublishedAt;
        existing.Slug = post.Slug;
        existing.Tags = post.Tags;
        existing.ImageUrls = post.ImageUrls;
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var post = _context.BlogPosts.Find(id);
        if (post == null)
            return;

        _context.BlogPosts.Remove(post);
        _context.SaveChanges();
    }
}
