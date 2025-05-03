using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Validators;
using Magato.Api.Repositories;
using System.Security.Cryptography;
using System.Text;


namespace Magato.Api.Services;

public class PageContentService : IPageContentService
{
    private readonly IPageContentRepository _repo;

    public PageContentService(IPageContentRepository repo)
    {
        _repo = repo;
    }

    public PageContentDto? Get(string key)
    {
        var content = _repo.Get(key);
        return content == null ? null : MapToDto(content);
    }

    public IEnumerable<PageContentDto> GetAll()
    {
        return _repo.GetAll().Select(MapToDto);
    }

    public void Add(PageContentDto dto)
    {
        var content = MapToEntity(dto);
        content.LastModified = DateTime.UtcNow;
        _repo.Add(content);
    }

    public void Update(PageContentDto dto)
    {
        var existing = _repo.Get(dto.Key);
        if (existing == null)
            return;

        existing.Title = dto.Title;
        existing.MainText = dto.MainText;
        existing.SubText = dto.SubText;
        existing.ExtraText = dto.ExtraText;
        existing.Published = dto.Published;
        existing.ImageUrls = dto.ImageUrls;
        existing.LastModified = DateTime.UtcNow;

        _repo.Update(existing);
    }

    public void Delete(string key)
    {
        _repo.Delete(key);
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
                Url = link.Url
            }).ToList()
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
                Url = link.Url
            }).ToList()

        };
    }
}
