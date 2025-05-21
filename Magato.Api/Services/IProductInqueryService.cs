// <copyright file="IProductInqueryService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
using Magato.Api.DTO;

public interface IProductInquiryService
{
    Task<ProductInquiryResponseDto> AddAsync(ProductInquiryCreateDto dto);

    Task<IEnumerable<ProductInquiryResponseDto>> GetAllAsync();

    Task<ProductInquiryResponseDto?> GetByIdAsync(int id);

    Task MarkAsHandledAsync(int id);
}
