// <copyright file="LookbookRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Data;
using Magato.Api.Models;

public sealed class LookbookRepository : ILookbookRepository
{
    private readonly ApplicationDbContext db;

    public LookbookRepository(ApplicationDbContext db) => this.db = db;

    public Task<LookbookImage?> GetByIdAsync(int id) => this.db.LookbookImages.FindAsync(id).AsTask();

    public async Task AddAsync(LookbookImage image)
    {
        this.db.LookbookImages.Add(image);
        await this.db.SaveChangesAsync();
    }

    public async Task UpdateAsync(LookbookImage image)
    {
        this.db.LookbookImages.Update(image);
        await this.db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.db.LookbookImages.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        this.db.LookbookImages.Remove(entity);
        await this.db.SaveChangesAsync();
    }
}
