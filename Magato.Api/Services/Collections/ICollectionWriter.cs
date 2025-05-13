// <copyright file="ICollectionWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
