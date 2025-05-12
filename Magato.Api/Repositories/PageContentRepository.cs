// <copyright file="PageContentRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;
using Magato.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class PageContentRepository : IPageContentRepository
{
    private readonly ApplicationDbContext context;

    public PageContentRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public PageContent? Get(string key)
    {
        return this.context.PageContents
          .Include(c => c.SocialMediaLinks)
          .FirstOrDefault(c => c.Key == key);
    }

    public IEnumerable<PageContent> GetAll()
    {
        return this.context.PageContents
         .Include(c => c.SocialMediaLinks)
         .ToList();
    }

    public void Add(PageContent content)
    {
        this.context.PageContents.Add(content);
        this.context.SaveChanges();
    }

    public void Update(PageContent content)
    {
        var existing = this.context.PageContents.FirstOrDefault(c => c.Key == content.Key);
        if (existing == null)
        {
            return;
        }

        existing.Title = content.Title;
        existing.MainText = content.MainText;
        existing.ExtraText = content.ExtraText;
        existing.ImageUrls = content.ImageUrls;
        existing.Published = content.Published;
        existing.LastModified = DateTime.UtcNow;
        existing.SocialMediaLinks.Clear();
        foreach (var link in content.SocialMediaLinks)
        {
            existing.SocialMediaLinks.Add(new SocialMediaLink
            {
                Platform = link.Platform,
                Url = link.Url,
            });
        }

        this.context.SaveChanges();
    }

    public void Delete(string key)
    {
        var content = this.context.PageContents.FirstOrDefault(c => c.Key == key);
        if (content == null)
        {
            return;
        }

        this.context.PageContents.Remove(content);
        this.context.SaveChanges();
    }
}
