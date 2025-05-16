// <copyright file="ILookbookWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;
using Magato.Api.DTO;

public interface ILookbookWriter
{
    Task<bool> AddAsync(int collectionId, LookbookImageDto dto);

    Task<bool> UpdateAsync(int imageId, LookbookImageDto dto);

    Task<bool> DeleteAsync(int imageId);
}
