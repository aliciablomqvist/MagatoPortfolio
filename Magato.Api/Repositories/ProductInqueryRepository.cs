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

    public IEnumerable<ProductInquiry> GetAll()
    {
        return this.context.ProductInquiries
            .Include(i => i.Product)
            .OrderByDescending(i => i.SentAt)
            .ToList();
    }

    public ProductInquiry? Get(int id)
    {
        return this.context.ProductInquiries
            .Include(i => i.Product)
            .FirstOrDefault(i => i.Id == id);
    }

    public void Add(ProductInquiry inquiry)
    {
        this.context.ProductInquiries.Add(inquiry);
        this.context.SaveChanges();
    }

    public void Update(ProductInquiry inquiry)
    {
        this.context.ProductInquiries.Update(inquiry);
        this.context.SaveChanges();
    }
}
