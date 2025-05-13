// <copyright file="IColorWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;

public interface IColorWriter
{
    // Colors
    Task<bool> AddAsync(int collectionId, ColorDto dto);

    Task<bool> UpdateAsync(int colorId, ColorDto dto);

    Task<bool> DeleteAsync(int colorId);
}
