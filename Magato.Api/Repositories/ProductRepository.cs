// <copyright file="ProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;
using Magato.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;

    public ProductRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public IEnumerable<Product> GetAll() =>
        this.context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToList();

    public Product? Get(int id) =>
        this.context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        this.context.Products.Add(product);
        this.context.SaveChanges();
    }

    public void Update(Product product)
    {
        this.context.Products.Update(product);
        this.context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = this.context.Products.Find(id);
        if (product != null)
        {
            this.context.Products.Remove(product);
            this.context.SaveChanges();
        }
    }
}
