// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository repo;

    public ProductService(IProductRepository repo)
    {
        this.repo = repo;
    }

    public IEnumerable<ProductDto> GetAll()
        => this.repo.GetAll().Select(Map);

    public ProductDto? Get(int id)
    {
        var product = this.repo.Get(id);
        return product == null ? null : Map(product);
    }

    public void Add(ProductDto dto)
        => this.repo.Add(Map(dto));

    public void Update(ProductDto dto)
        => this.repo.Update(Map(dto));

    public void Delete(int id)
        => this.repo.Delete(id);

    private static ProductDto Map(Product p) => new ()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Price = p.Price,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name ?? string.Empty,
        ImageUrls = p.ProductImages.Select(i => i.ImageUrl).ToList(),
        Status = p.Status,
    };

    private static Product Map(ProductDto dto) => new ()
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        CategoryId = dto.CategoryId,
        ProductImages = dto.ImageUrls.Select(url => new ProductImage { ImageUrl = url }).ToList(),
        Status = dto.Status,
    };
}
