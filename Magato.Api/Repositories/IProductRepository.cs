// <copyright file="IProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories;
public interface IProductRepository
{
    IEnumerable<Product> GetAll();

    Product? Get(int id);

    void Add(Product product);

    void Update(Product product);

    void Delete(int id);
}
