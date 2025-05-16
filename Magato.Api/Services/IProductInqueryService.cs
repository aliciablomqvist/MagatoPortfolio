// <copyright file="IProductInqueryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;

namespace Magato.Api.Services;

public interface IProductInquiryService
{
    Task<ProductInquiryResponseDto> AddAsync(ProductInquiryCreateDto dto);

    Task<IEnumerable<ProductInquiryResponseDto>> GetAllAsync();

    Task<ProductInquiryResponseDto?> GetByIdAsync(int id);

    Task MarkAsHandledAsync(int id);
}
