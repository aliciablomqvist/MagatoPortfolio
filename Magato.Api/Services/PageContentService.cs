// <copyright file="PageContentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
public class PageContentService : IPageContentService
{
    private readonly IPageContentRepository repo;

    public PageContentService(IPageContentRepository repo)
    {
        this.repo = repo;
    }

    public PageContentDto? Get(string key)
    {
        var content = this.repo.Get(key);
        return content == null ? null : MapToDto(content);
    }

    public IEnumerable<PageContentDto> GetAll()
    {
        return this.repo.GetAll().Select(MapToDto);
    }

    public void Add(PageContentDto dto)
    {
        var content = MapToEntity(dto);
        content.LastModified = DateTime.UtcNow;
        this.repo.Add(content);
    }

    public void Update(PageContentDto dto)
    {
        var existing = this.repo.Get(dto.Key);
        if (existing == null)
        {
            return;
        }

        existing.Title = dto.Title;
        existing.MainText = dto.MainText;
        existing.SubText = dto.SubText;
        existing.ExtraText = dto.ExtraText;
        existing.Published = dto.Published;
        existing.ImageUrls = dto.ImageUrls;
        existing.LastModified = DateTime.UtcNow;

        this.repo.Update(existing);
    }

    public void Delete(string key)
    {
        this.repo.Delete(key);
    }

    private static PageContentDto MapToDto(PageContent entity)
    {
        return new PageContentDto
        {
            Key = entity.Key,
            Title = entity.Title,
            MainText = entity.MainText,
            SubText = entity.SubText,
            ExtraText = entity.ExtraText,
            Published = entity.Published,
            LastModified = entity.LastModified,
            ImageUrls = entity.ImageUrls,
            SocialMediaLinks = entity.SocialMediaLinks
            .Select(link => new SocialMediaLinkDto
            {
                Platform = link.Platform,
                Url = link.Url,
            }).ToList(),
        };
    }

    private static PageContent MapToEntity(PageContentDto dto)
    {
        return new PageContent
        {
            Key = dto.Key,
            Title = dto.Title,
            MainText = dto.MainText,
            SubText = dto.SubText,
            ExtraText = dto.ExtraText,
            Published = dto.Published,
            LastModified = dto.LastModified,
            ImageUrls = dto.ImageUrls,
            SocialMediaLinks = dto.SocialMediaLinks
            .Select(link => new SocialMediaLink
            {
                Platform = link.Platform,
                Url = link.Url,
            }).ToList(),
        };
    }
}
