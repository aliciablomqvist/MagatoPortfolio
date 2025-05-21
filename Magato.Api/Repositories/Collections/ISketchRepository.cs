// <copyright file="ISketchRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Models;

public interface ISketchRepository
{
    Task<Sketch?> GetByIdAsync(int id);

    Task AddAsync(Sketch sketch);

    Task UpdateAsync(Sketch sketch);

    Task DeleteAsync(int id);
}
