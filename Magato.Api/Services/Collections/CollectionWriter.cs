// <copyright file="CollectionWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

public sealed class CollectionWriter : ICollectionWriter
{
    private readonly ICollectionRepository repo;

    public CollectionWriter(ICollectionRepository repo) => this.repo = repo;

    public async Task<Collection> AddAsync(CollectionCreateDto dto)
    {
        var col = Map(dto);
        await this.repo.AddAsync(col);
        return col;
    }

    public async Task<bool> UpdateAsync(int id, CollectionDto dto)
    {
        var col = await this.repo.GetByIdAsync(id);
        if (col is null)
        {
            return false;
        }

        Map(dto, col);
        await this.repo.UpdateAsync(col);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var col = await this.repo.GetByIdAsync(id);
        if (col is null)
        {
            return false;
        }

        await this.repo.DeleteAsync(id);
        return true;
    }

    private static Collection Map(CollectionCreateDto dto) => new()
    {
        CollectionTitle = dto.CollectionTitle,
        CollectionDescription = dto.CollectionDescription,
        ReleaseDate = dto.ReleaseDate,
        Colors = dto.Colors.Select(c => new ColorOption { Name = c.Name, Hex = c.Hex }).ToList(),
        Materials = dto.Materials.Select(m => new Material { Name = m.Name, Description = m.Description }).ToList(),
        Sketches = dto.Sketches.Select(s => new Sketch { Url = s.Url }).ToList(),
        LookbookImages = dto.LookbookImages.Select(l => new LookbookImage
        {
            Url = l.Url,
            Description = l.Description
        }).ToList()
    };


    private static void Map(CollectionDto dto, Collection target)
    {
        target.CollectionTitle = dto.CollectionTitle;
        target.CollectionDescription = dto.CollectionDescription;
        target.ReleaseDate = dto.ReleaseDate;
        target.Colors = dto.Colors.Select(c => new ColorOption { Name = c.Name, Hex = c.Hex }).ToList();
        target.Materials = dto.Materials.Select(m => new Material { Name = m.Name, Description = m.Description }).ToList();
        target.Sketches = dto.Sketches.Select(s => new Sketch { Url = s.Url }).ToList();
        target.LookbookImages = dto.LookbookImages.Select(l => new LookbookImage
        {
            Url = l.Url,
            Description = l.Description
        }).ToList();
    }
}
