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
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
                .ToListAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
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

        // Color
        public async Task<ColorOption?> GetColorByIdAsync(int id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task UpdateColorAsync(ColorOption color)
        {
            _context.Colors.Update(color);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteColorAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
            }
        }

        // Material
        public async Task<Material?> GetMaterialAsync(int materialId)
        {
            return await _context.Materials.FindAsync(materialId);
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaterialAsync(int materialId)
        {
            var material = await _context.Materials.FindAsync(materialId);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }

        // Sketch
        public async Task<Sketch?> GetSketchAsync(int sketchId)
        {
            return await _context.Sketches.FindAsync(sketchId);
        }

        public async Task UpdateSketchAsync(Sketch sketch)
        {
            _context.Sketches.Update(sketch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSketchAsync(int sketchId)
        {
            var sketch = await _context.Sketches.FindAsync(sketchId);
            if (sketch != null)
            {
                _context.Sketches.Remove(sketch);
                await _context.SaveChangesAsync();
            }
        }

        // LookbookImage
        public async Task<LookbookImage?> GetLookbookImageAsync(int imageId)
        {
            return await _context.LookbookImages.FindAsync(imageId);
        }

        public async Task AddLookbookImageAsync(int collectionId, LookbookImage image)
        {
            image.CollectionId = collectionId;
            _context.LookbookImages.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLookbookImageAsync(LookbookImage image)
        {
            _context.LookbookImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLookbookImageAsync(int imageId)
        {
            var image = await _context.LookbookImages.FindAsync(imageId);
            if (image != null)
            {
                _context.LookbookImages.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}

