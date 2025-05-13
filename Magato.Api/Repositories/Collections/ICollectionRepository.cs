// <copyright file="ICollectionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;
using Magato.Api.Models;

namespace Magato.Api.Repositories.Collections;
public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetAllAsync();

    Task<Collection?> GetByIdAsync(int id);

    Task AddAsync(Collection collection);

    Task UpdateAsync(Collection collection);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
