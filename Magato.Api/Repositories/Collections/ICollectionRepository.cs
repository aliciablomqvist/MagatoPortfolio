// <copyright file="ICollectionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories.Collections;

using Magato.Api.Models;

public interface ICollectionRepository
{
    Task<IEnumerable<Collection>> GetAllAsync();

    Task<Collection?> GetByIdAsync(int id);

    Task AddAsync(Collection collection);

    Task UpdateAsync(Collection collection);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
