// <copyright file="LookbookWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

public sealed class LookbookWriter : ILookbookWriter
{
    private readonly ILookbookRepository lookbooks;
    private readonly ICollectionRepository collections;

    public LookbookWriter(
        ILookbookRepository lookbooks,
        ICollectionRepository collections)
{
        this.lookbooks = lookbooks;
        this.collections = collections;
    }

    public async Task<bool> AddAsync(int collectionId, LookbookImageDto dto)
{
        var col = await this.collections.GetByIdAsync(collectionId);
        if (col is null)
{
            return false;
        }

        var image = new LookbookImage
{
            Url = dto.Url,
            Description = dto.Description,
            CollectionId = collectionId,
        };

        await this.lookbooks.AddAsync(image);
        return true;
    }

    public async Task<bool> UpdateAsync(int imageId, LookbookImageDto dto)
{
        var image = await this.lookbooks.GetByIdAsync(imageId);
        if (image is null)
{
            return false;
        }

        image.Url = dto.Url;
        image.Description = dto.Description;

        await this.lookbooks.UpdateAsync(image);
        return true;
    }

    public async Task<bool> DeleteAsync(int imageId)
{
        var image = await this.lookbooks.GetByIdAsync(imageId);
        if (image is null)
{
            return false;
        }

        await this.lookbooks.DeleteAsync(imageId);
        return true;
    }
}
