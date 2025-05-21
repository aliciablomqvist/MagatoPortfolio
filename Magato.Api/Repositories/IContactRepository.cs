// <copyright file="IContactRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Models;
public interface IContactRepository
{
    Task AddAsync(ContactMessage message);

    Task<IEnumerable<ContactMessage>> GetAllAsync();

    Task<bool> DeleteAsync(int id);
}
