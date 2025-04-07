using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;
public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;

    public ContactRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ContactMessage message)
    {
        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContactMessage>> GetAllAsync()
    {
        return await _context.ContactMessages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }
}
