// <copyright file="ProductInqueryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;

public class ProductInquiryService : IProductInquiryService
{
    private readonly IProductInquiryRepository repo;
    private readonly IProductRepository productRepo;

    public ProductInquiryService(IProductInquiryRepository repo, IProductRepository productRepo)
    {
        this.repo = repo;
        this.productRepo = productRepo;
    }

    public async Task<ProductInquiryResponseDto> AddAsync(ProductInquiryCreateDto dto)
    {
        var product = await this.productRepo.GetByIdAsync(dto.ProductId);
        if (product == null)
        {
            throw new InvalidOperationException($"Product with ID {dto.ProductId} was not found.");
        }

        var inquiry = new ProductInquiry
        {
            ProductId = dto.ProductId,
            Email = dto.Email,
            Message = dto.Message,
            Size = dto.Size,
        };

        await this.repo.AddAsync(inquiry);

        return new ProductInquiryResponseDto
        {
            //Id = inquiry.Id,
            ProductTitle = product.Title,
            Email = inquiry.Email,
            Message = inquiry.Message,
            Size = inquiry.Size,
            SentAt = inquiry.SentAt,
            // IsHandled = false,
        };
    }

    public async Task<IEnumerable<ProductInquiryResponseDto>> GetAllAsync()
    {
        var inquiries = await this.repo.GetAllAsync();

        return inquiries.Select(i => new ProductInquiryResponseDto
        {
            Id = i.Id,
            ProductTitle = i.Product?.Title ?? "Unknown",
            Email = i.Email,
            Message = i.Message,
            Size = i.Size,
            SentAt = i.SentAt,
            IsHandled = i.IsHandled,
        });
    }

    public async Task<ProductInquiryResponseDto?> GetByIdAsync(int id)
    {
        var inquiry = await this.repo.GetByIdAsync(id);
        if (inquiry == null)
        {
            return null;
        }

        return new ProductInquiryResponseDto
        {
            Id = inquiry.Id,
            ProductTitle = inquiry.Product?.Title ?? "Unknown",
            Email = inquiry.Email,
            Message = inquiry.Message,
            Size = inquiry.Size,
            SentAt = inquiry.SentAt,
            IsHandled = inquiry.IsHandled,
        };
    }

    public async Task MarkAsHandledAsync(int id)
    {
        var inquiry = await this.repo.GetByIdAsync(id);
        if (inquiry != null)
        {
            inquiry.IsHandled = true;
            await this.repo.UpdateAsync(inquiry);
        }
    }
}
