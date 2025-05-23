// <copyright file="IProductService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
using Magato.Api.DTO;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<IEnumerable<ProductDto>> GetForSaleAsync();

    Task<ProductDto?> GetByIdAsync(int id);

    Task AddAsync(ProductDto dto);

    Task UpdateAsync(ProductDto dto);

    Task DeleteAsync(int id);
}
