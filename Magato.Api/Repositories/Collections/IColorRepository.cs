// <copyright file="IColorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories.Collections;
using Magato.Api.Models;
public interface IColorRepository
{
    Task<ColorOption?> GetByIdAsync(int id);

    Task AddAsync(ColorOption color);

    Task UpdateAsync(ColorOption color);

    Task DeleteAsync(int id);
}
