// <copyright file="ICollectionReader.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.DTO;
using Magato.Api.Models;

// *** Query‑interface (read‑only) ***
public interface ICollectionReader
{
    Task<IEnumerable<Collection>> GetAllAsync();

    Task<Collection?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int id);
}
