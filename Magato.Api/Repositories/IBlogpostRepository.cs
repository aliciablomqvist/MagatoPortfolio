// <copyright file="IBlogpostRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories;
public interface IBlogPostRepository
{
    BlogPost? Get(int id);

    BlogPost? GetBySlug(string slug);

    IEnumerable<BlogPost> GetAll();

    void Add(BlogPost post);

    void Update(BlogPost post);

    void Delete(int id);

    IEnumerable<BlogPost> GetByTag(string tag);
}
