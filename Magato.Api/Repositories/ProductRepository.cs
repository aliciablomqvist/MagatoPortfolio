// <copyright file="ProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;

    public ProductRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await this.Query().ToListAsync();

    public async Task<IEnumerable<Product>> GetForSaleAsync()
        => await this.Query().Where(p => p.IsForSale).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id)
        => await this.Query().FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Product product)
    {
        this.context.Products.Add(product);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        this.context.Products.Update(product);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await this.context.Products.FindAsync(id);
        if (product != null)
        {
            this.context.Products.Remove(product);
            await this.context.SaveChangesAsync();
        }
    }

    private IQueryable<Product> Query()
        => this.context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .AsNoTracking();
}
