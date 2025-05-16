// <copyright file="IProductInqueryRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface IProductInquiryRepository
{
    Task<IEnumerable<ProductInquiry>> GetAllAsync();

    Task<ProductInquiry?> GetByIdAsync(int id);

    Task AddAsync(ProductInquiry inquiry);

    Task UpdateAsync(ProductInquiry inquiry);
}
