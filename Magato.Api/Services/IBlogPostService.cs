// <copyright file="IBlogPostService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;

namespace Magato.Api.Services;
public interface IBlogPostService
{
    BlogPostDto? Get(int id);

    BlogPostDto? GetBySlug(string slug);

    IEnumerable<BlogPostDto> GetAll();

    void Add(BlogPostDto dto);

    void Update(BlogPostDto dto);

    void Delete(int id);
}
