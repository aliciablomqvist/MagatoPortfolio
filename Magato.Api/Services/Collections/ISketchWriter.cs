// <copyright file="ISketchWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;

public interface ISketchWriter
{
    Task<bool> AddAsync(int collectionId, SketchDto dto);

    Task<bool> UpdateAsync(int sketchId, SketchDto dto);

    Task<bool> DeleteAsync(int sketchId);
}
