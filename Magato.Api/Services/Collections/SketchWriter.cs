// <copyright file="SketchWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

public sealed class SketchWriter : ISketchWriter
{
    private readonly ISketchRepository sketches;
    private readonly ICollectionRepository collections;

    public SketchWriter(
        ISketchRepository sketches,
        ICollectionRepository collections)
    {
        this.sketches = sketches;
        this.collections = collections;
    }

    public async Task<bool> AddAsync(int collectionId, SketchDto dto)
    {
        var col = await this.collections.GetByIdAsync(collectionId);
        if (col is null)
        {
            return false;
        }

        var sketch = new Sketch
        {
            Url = dto.Url,
            CollectionId = collectionId,
        };

        await this.sketches.AddAsync(sketch);
        return true;
    }

    public async Task<bool> UpdateAsync(int sketchId, SketchDto dto)
    {
        var sketch = await this.sketches.GetByIdAsync(sketchId);
        if (sketch is null)
        {
            return false;
        }

        sketch.Url = dto.Url;
        await this.sketches.UpdateAsync(sketch);
        return true;
    }

    public async Task<bool> DeleteAsync(int sketchId)
    {
        var sketch = await this.sketches.GetByIdAsync(sketchId);
        if (sketch is null)
        {
            return false;
        }

        await this.sketches.DeleteAsync(sketchId);
        return true;
    }
}
