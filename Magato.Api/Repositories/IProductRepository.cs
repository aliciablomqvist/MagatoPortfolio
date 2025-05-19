// <copyright file="IProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<IEnumerable<Product>> GetForSaleAsync();

    Task<Product?> GetByIdAsync(int id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(int id);
}
