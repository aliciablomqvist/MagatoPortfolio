// <copyright file="ILookbookRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories.Collections;
public interface ILookbookRepository
{
    Task<LookbookImage?> GetByIdAsync(int id);

    Task AddAsync(LookbookImage image);

    Task UpdateAsync(LookbookImage image);

    Task DeleteAsync(int id);
}
