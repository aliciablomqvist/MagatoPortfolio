// <copyright file="ISketchWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;

public interface ISketchWriter
{
    Task<bool> AddAsync(int collectionId, SketchDto dto);

    Task<bool> UpdateAsync(int sketchId, SketchDto dto);

    Task<bool> DeleteAsync(int sketchId);
}
