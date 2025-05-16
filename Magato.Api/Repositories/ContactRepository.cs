// <copyright file="ContactRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext context;

    public ContactRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(ContactMessage message)
    {
        this.context.ContactMessages.Add(message);
        await this.context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContactMessage>> GetAllAsync()
    {
        return await this.context.ContactMessages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var message = await this.context.ContactMessages.FindAsync(id);
        if (message == null)
        {
            return false;
        }

        this.context.ContactMessages.Remove(message);
        await this.context.SaveChangesAsync();
        return true;
    }
}
