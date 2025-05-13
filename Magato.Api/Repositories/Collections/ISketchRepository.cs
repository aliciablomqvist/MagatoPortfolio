// <copyright file="ISketchRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories.Collections;
public interface ISketchRepository
{
    Task<Sketch?> GetByIdAsync(int id);

    Task AddAsync(Sketch sketch);

    Task UpdateAsync(Sketch sketch);

    Task DeleteAsync(int id);
}
