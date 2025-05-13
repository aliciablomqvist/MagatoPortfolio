// <copyright file="IColorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories.Collections;
public interface IColorRepository
{
    Task<ColorOption?> GetByIdAsync(int id);

    Task AddAsync(ColorOption color);

    Task UpdateAsync(ColorOption color);

    Task DeleteAsync(int id);
}
