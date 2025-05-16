// <copyright file="CategoryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repo;

    public CategoryService(ICategoryRepository repo)
    {
        this.repo = repo;
    }

    public IEnumerable<CategoryDto> GetAll()
        => this.repo.GetAll().Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
        });

    public CategoryDto? GetById(int id)
    {
        var category = this.repo.GetById(id);
        if (category == null)
        {
            return null;
        }

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public void Add(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
        };

        this.repo.Add(category);
    }

    public void Update(int id, CategoryDto dto)
    {
        var category = this.repo.GetById(id);
        if (category == null)
        {
            return;
        }

        category.Name = dto.Name;
        this.repo.Update(category);
    }

    public void Delete(int id)
    {
        var category = this.repo.GetById(id);
        if (category == null)
        {
            return;
        }

        this.repo.Delete(category);
    }
}
