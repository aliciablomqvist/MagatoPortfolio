// <copyright file="MaterialWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

public sealed class MaterialWriter : IMaterialWriter
{
    private readonly IMaterialRepository materials;
    private readonly ICollectionRepository collections;

    public MaterialWriter(
        IMaterialRepository materials,
        ICollectionRepository collections)
{
        this.materials = materials;
        this.collections = collections;
    }

    public async Task<bool> AddAsync(int collectionId, MaterialDto dto)
{
        var col = await this.collections.GetByIdAsync(collectionId);
        if (col is null)
{
            return false;
        }

        var material = new Material
{
            Name = dto.Name,
            Description = dto.Description,
            CollectionId = collectionId,
        };

        await this.materials.AddAsync(material);
        return true;
    }

    public async Task<bool> UpdateAsync(int materialId, MaterialDto dto)
{
        var material = await this.materials.GetByIdAsync(materialId);
        if (material is null)
{
            return false;
        }

        material.Name = dto.Name;
        material.Description = dto.Description;

        await this.materials.UpdateAsync(material);
        return true;
    }

    public async Task<bool> DeleteAsync(int materialId)
{
        var material = await this.materials.GetByIdAsync(materialId);
        if (material is null)
{
            return false;
        }

        await this.materials.DeleteAsync(materialId);
        return true;
    }
}
