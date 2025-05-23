// <copyright file="SketchRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Data;
using Magato.Api.Models;

public sealed class SketchRepository : ISketchRepository
{
    private readonly ApplicationDbContext db;

    public SketchRepository(ApplicationDbContext db) => this.db = db;

    public Task<Sketch?> GetByIdAsync(int id) => this.db.Sketches.FindAsync(id).AsTask();

    public async Task AddAsync(Sketch sketch)
    {
        this.db.Sketches.Add(sketch);
        await this.db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sketch sketch)
    {
        this.db.Sketches.Update(sketch);
        await this.db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await this.db.Sketches.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        this.db.Sketches.Remove(entity);
        await this.db.SaveChangesAsync();
    }
}
