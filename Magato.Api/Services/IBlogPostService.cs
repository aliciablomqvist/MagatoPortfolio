// <copyright file="IBlogPostService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;

public interface IBlogPostService
{
    BlogPostDto? Get(int id);

    BlogPostDto? GetBySlug(string slug);

    IEnumerable<BlogPostDto> GetAll();

    void Add(BlogPostDto dto);

    void Update(BlogPostDto dto);

    void Delete(int id);
}
