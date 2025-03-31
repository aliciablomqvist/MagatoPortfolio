
using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDbContext _context;

        public CollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await _context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                //.Include(c => c.Images) Hur hantera lookbook och bilder?
                .Include(c => c.Sketches)
                .ToListAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
               // .Include(c => c.Images)
                .Include(c => c.Sketches)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCollectionAsync(Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCollectionAsync(Collection collection)
        {
            _context.Collections.Update(collection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCollectionAsync(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CollectionExistsAsync(int id)
        {
            return await _context.Collections.AnyAsync(c => c.Id == id);
        }


    }

}