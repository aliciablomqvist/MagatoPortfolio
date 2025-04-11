using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class PageContentRepository : IPageContentRepository
{
    private readonly ApplicationDbContext _context;

    public PageContentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public PageContent? Get(string key)
    {
        return _context.PageContents.FirstOrDefault(c => c.Key == key);
    }

    public IEnumerable<PageContent> GetAll()
    {
        return _context.PageContents.ToList();
    }

    public void Add(PageContent content)
    {
        _context.PageContents.Add(content);
        _context.SaveChanges();
    }

    public void Update(PageContent content)
    {
        var existing = _context.PageContents.FirstOrDefault(c => c.Key == content.Key);
        if (existing == null)
            return;

        existing.Title = content.Title;
        existing.MainText = content.MainText;
        existing.ExtraText = content.ExtraText;
        existing.MediaUrls = content.MediaUrls;
        existing.Published = content.Published;
        existing.LastModified = DateTime.UtcNow;

        _context.SaveChanges();
    }


    public void Delete(string key)
    {
        var content = _context.PageContents.FirstOrDefault(c => c.Key == key);
        if (content == null)
            return;

        _context.PageContents.Remove(content);
        _context.SaveChanges();
    }
}
