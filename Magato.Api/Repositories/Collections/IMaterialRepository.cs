// <copyright file="IMaterialRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories.Collections;
public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(int id);

    Task AddAsync(Material material);

    Task UpdateAsync(Material material);

    Task DeleteAsync(int id);
}
