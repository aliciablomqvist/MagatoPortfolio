// <copyright file="IColorWriter.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;

public interface IColorWriter
{
    // Colors
    Task<bool> AddAsync(int collectionId, ColorDto dto);

    Task<bool> UpdateAsync(int colorId, ColorDto dto);

    Task<bool> DeleteAsync(int colorId);
}
