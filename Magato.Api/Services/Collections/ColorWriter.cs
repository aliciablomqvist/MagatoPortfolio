// <copyright file="ColorWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

namespace Magato.Api.Services.Collections;
public sealed class ColorWriter : IColorWriter
{
    private readonly IColorRepository colors;
    private readonly ICollectionRepository collections;

    public ColorWriter(IColorRepository colors, ICollectionRepository collections)
    {
        this.colors = colors;
        this.collections = collections;
    }

    public async Task<bool> AddAsync(int collectionId, ColorDto dto)
    {
        var col = await this.collections.GetByIdAsync(collectionId);
        if (col is null)
        {
            return false;
        }

        var color = new ColorOption { Name = dto.Name, Hex = dto.Hex, CollectionId = collectionId };
        await this.colors.AddAsync(color);
        return true;
    }

    public async Task<bool> UpdateAsync(int colorId, ColorDto dto)
    {
        var color = await this.colors.GetByIdAsync(colorId);
        if (color is null)
        {
            return false;
        }

        color.Name = dto.Name;
        color.Hex = dto.Hex;
        await this.colors.UpdateAsync(color);
        return true;
    }

    public async Task<bool> DeleteAsync(int colorId)
    {
        var color = await this.colors.GetByIdAsync(colorId);
        if (color is null)
        {
            return false;
        }

        await this.colors.DeleteAsync(colorId);
        return true;
    }
}
