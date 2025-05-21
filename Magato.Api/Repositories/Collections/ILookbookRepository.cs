// <copyright file="ILookbookRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Models;
public interface ILookbookRepository
{
    Task<LookbookImage?> GetByIdAsync(int id);

    Task AddAsync(LookbookImage image);

    Task UpdateAsync(LookbookImage image);

    Task DeleteAsync(int id);
}
