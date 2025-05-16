// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository repo;

    public ProductService(IProductRepository repo)
    {
        this.repo = repo;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
        => (await this.repo.GetAllAsync()).Select(Map);

    public async Task<IEnumerable<ProductDto>> GetForSaleAsync()
        => (await this.repo.GetForSaleAsync()).Select(Map);

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await this.repo.GetByIdAsync(id);
        return product == null ? null : Map(product);
    }

    public async Task AddAsync(ProductDto dto)
        => await this.repo.AddAsync(Map(dto));

    public async Task UpdateAsync(ProductDto dto)
        => await this.repo.UpdateAsync(Map(dto));

    public async Task DeleteAsync(int id)
        => await this.repo.DeleteAsync(id);

    private static ProductDto Map(Product p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Price = p.Price,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name ?? string.Empty,
        ImageUrls = p.ProductImages.Select(i => i.ImageUrl).ToList(),
        Status = p.Status,
        IsForSale = p.IsForSale,
    };

    private static Product Map(ProductDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        CategoryId = dto.CategoryId,
        IsForSale = dto.IsForSale,
        ProductImages = dto.ImageUrls.Select(url => new ProductImage { ImageUrl = url }).ToList(),
        Status = dto.Status,
    };
}
