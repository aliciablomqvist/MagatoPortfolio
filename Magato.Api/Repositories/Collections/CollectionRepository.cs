// <copyright file="CollectionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Microsoft.EntityFrameworkCore;
using Magato.Api.Data;
using Magato.Api.Models;

public sealed class CollectionRepository : ICollectionRepository
{
    private readonly ApplicationDbContext db;

    public CollectionRepository(ApplicationDbContext db) => this.db = db;

    public async Task<IEnumerable<Collection>> GetAllAsync()
    {
        return await this.db.Collections
            .Include(c => c.Colors)
            .Include(c => c.Materials)
            .Include(c => c.Sketches)
            .Include(c => c.LookbookImages)
            .AsNoTracking()
            .ToListAsync();          // List<Collection>   ← implicit up‑cast till IEnumerable
    }

    public Task<Collection?> GetByIdAsync(int id) =>
        this.db.Collections
          .Include(c => c.Colors)
          .Include(c => c.Materials)
          .Include(c => c.Sketches)
          .Include(c => c.LookbookImages)
          .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Collection collection)
    {
        this.db.Collections.Add(collection);
        await this.db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Collection collection)
    {
        this.db.Collections.Update(collection);
        await this.db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.db.Collections.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        this.db.Collections.Remove(entity);
        await this.db.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        this.db.Collections.AnyAsync(c => c.Id == id);
}
