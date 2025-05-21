// <copyright file="ICollectionWriter.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

// *** Command‑interface  ***
public interface ICollectionWriter
{
    Task<Collection> AddAsync(CollectionCreateDto dto);

    Task<bool> UpdateAsync(int id, CollectionDto dto);

    Task<bool> DeleteAsync(int id);
}
