// <copyright file="CategoryRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext context;

    public CategoryRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Category> GetAll() => this.context.Categories.ToList();

    public Category? GetById(int id) => this.context.Categories.FirstOrDefault(c => c.Id == id);

    public void Add(Category category)
    {
        this.context.Categories.Add(category);
        this.context.SaveChanges();
    }

    public void Update(Category category)
    {
        this.context.Categories.Update(category);
        this.context.SaveChanges();
    }

    public void Delete(Category category)
    {
        this.context.Categories.Remove(category);
        this.context.SaveChanges();
    }
}
