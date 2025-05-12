// <copyright file="IContactRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;
using Magato.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;
public interface IContactRepository
{
    Task AddAsync(ContactMessage message);

    Task<IEnumerable<ContactMessage>> GetAllAsync();

    Task<bool> DeleteAsync(int id);
}
