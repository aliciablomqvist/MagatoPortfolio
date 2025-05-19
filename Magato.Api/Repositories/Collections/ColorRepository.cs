// <copyright file="ColorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Data;
using Magato.Api.Models;

public sealed class ColorRepository : IColorRepository
{
    private readonly ApplicationDbContext db;

    public ColorRepository(ApplicationDbContext db) => this.db = db;

    public Task<ColorOption?> GetByIdAsync(int id) =>
        this.db.Colors.FindAsync(id).AsTask();

    public async Task AddAsync(ColorOption color)
{
        this.db.Colors.Add(color);
        await this.db.SaveChangesAsync();
    }

    public async Task UpdateAsync(ColorOption color)
{
        this.db.Colors.Update(color);
        await this.db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
{
        var entity = await this.db.Colors.FindAsync(id);
        if (entity is null)
{
            return;
        }

        this.db.Colors.Remove(entity);
        await this.db.SaveChangesAsync();
    }
}
