using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;
public interface IContactRepository
{
    Task AddAsync(ContactMessage message);
    Task<IEnumerable<ContactMessage>> GetAllAsync();
}
