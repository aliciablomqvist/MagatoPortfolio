// <copyright file="MaterialRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Data;
using Magato.Api.Models;

public sealed class MaterialRepository : IMaterialRepository
{
    private readonly ApplicationDbContext db;

    public MaterialRepository(ApplicationDbContext db) => this.db = db;

    public Task<Material?> GetByIdAsync(int id) => this.db.Materials.FindAsync(id).AsTask();

    public async Task AddAsync(Material material)
    {
        this.db.Materials.Add(material);
        await this.db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Material material)
    {
        this.db.Materials.Update(material);
        await this.db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.db.Materials.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        this.db.Materials.Remove(entity);
        await this.db.SaveChangesAsync();
    }
}
