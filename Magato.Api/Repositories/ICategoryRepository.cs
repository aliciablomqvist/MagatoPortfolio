// <copyright file="ICategoryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Models;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();

    Category? GetById(int id);

    void Add(Category category);

    void Update(Category category);

    void Delete(Category category);
}
