// <copyright file="IProductInqueryRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Models;

public interface IProductInquiryRepository
{
    Task<IEnumerable<ProductInquiry>> GetAllAsync();

    Task<ProductInquiry?> GetByIdAsync(int id);

    Task AddAsync(ProductInquiry inquiry);

    Task UpdateAsync(ProductInquiry inquiry);
}
