// <copyright file="IBlogpostRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Models;

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
