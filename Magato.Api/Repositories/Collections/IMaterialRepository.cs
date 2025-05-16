// <copyright file="IMaterialRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Models;
public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(int id);

    Task AddAsync(Material material);

    Task UpdateAsync(Material material);

    Task DeleteAsync(int id);
}
