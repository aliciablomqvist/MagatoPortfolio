// <copyright file="ProductInqueryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class ProductInquiryRepository : IProductInquiryRepository
{
    private readonly ApplicationDbContext context;

    public ProductInquiryRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ProductInquiry>> GetAllAsync()
    {
        return await this.context.ProductInquiries
            .Include(i => i.Product)
            .OrderByDescending(i => i.SentAt)
            .ToListAsync();
    }

    public async Task<ProductInquiry?> GetByIdAsync(int id)
    {
        return await this.context.ProductInquiries
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddAsync(ProductInquiry inquiry)
    {
        this.context.ProductInquiries.Add(inquiry);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductInquiry inquiry)
    {
        this.context.ProductInquiries.Update(inquiry);
        await this.context.SaveChangesAsync();
    }
}
