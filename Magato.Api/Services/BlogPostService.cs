// <copyright file="BlogPostService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;
public class BlogPostService : IBlogPostService
{
    private readonly IBlogPostRepository repo;

    public BlogPostService(IBlogPostRepository repo)
    {
        this.repo = repo;
    }

    public BlogPostDto? Get(int id)
    {
        var post = this.repo.Get(id);
        return post == null ? null : Map(post);
    }

    public IEnumerable<BlogPostDto> GetAll()
        => this.repo.GetAll().Select(Map);

    public void Add(BlogPostDto dto)
    {
        dto.Slug = GenerateSlug(dto.Title);
        this.repo.Add(Map(dto));
    }

    public void Update(BlogPostDto dto)
    {
        dto.Slug = GenerateSlug(dto.Title);
        this.repo.Update(Map(dto));
    }

    public void Delete(int id)
            => this.repo.Delete(id);

    private static BlogPostDto Map(BlogPost post) => new ()
    {
        Id = post.Id,
        Title = post.Title,
        Slug = post.Slug,
        Content = post.Content,
        Author = post.Author,
        PublishedAt = post.PublishedAt,
        Tags = post.Tags,
        ImageUrls = post.ImageUrls,
    };

    private static BlogPost Map(BlogPostDto dto) => new ()
    {
        Id = dto.Id,
        Title = dto.Title,
        Slug = dto.Slug,
        Content = dto.Content,
        Author = dto.Author,
        PublishedAt = dto.PublishedAt,
        Tags = dto.Tags,
        ImageUrls = dto.ImageUrls,
    };

    private static string GenerateSlug(string title)
    {
        return title.ToLower()
            .Replace(" ", "-")
            .Replace("å", "a")
            .Replace("ä", "a")
            .Replace("ö", "o")
            .Replace(".", string.Empty)
            .Replace(",", string.Empty)
            .Replace("!", string.Empty)
            .Replace("?", string.Empty)
            .Replace(":", string.Empty)
            .Replace(";", string.Empty)
            .Replace("/", "-")
            .Replace("\\", "-");
    }

    public BlogPostDto? GetBySlug(string slug)
    {
        var post = this.repo.GetBySlug(slug);
        return post == null ? null : Map(post);
    }
}
