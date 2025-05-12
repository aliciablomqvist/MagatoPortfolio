using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories
{

    /// <summary>
    /// Handles database operations related to collections and their associated data.
    /// </summary>
    public class CollectionRepository : ICollectionRepository
    {
        private readonly ApplicationDbContext _context;

        public CollectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all collections including related data (colors, materials, sketches, lookbook images).
        /// </summary>
        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await _context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection by ID including related entities.
        /// </summary>
        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Adds a new collection to the database.
        /// </summary>

        public async Task AddCollectionAsync(Collection collection)
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing collection in the database.
        /// </summary>
        public async Task UpdateCollectionAsync(Collection collection)
        {
            _context.Collections.Update(collection);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Deletes a collection by ID if it exists.
        /// </summary>
        public async Task DeleteCollectionAsync(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks whether a collection with the specified ID exists.
        /// </summary>
        public async Task<bool> CollectionExistsAsync(int id)
        {
            return await _context.Collections.AnyAsync(c => c.Id == id);
        }

        // Colors
        /// <summary>Fetches a color by ID.</summary>
        public async Task<ColorOption?> GetColorByIdAsync(int id)
        {
            return await _context.Colors.FindAsync(id);
        }

        /// <summary>Updates an existing color.</summary>
        public async Task UpdateColorAsync(ColorOption color)
        {
            _context.Colors.Update(color);
            await _context.SaveChangesAsync();
        }


        /// <summary>Deletes a color by ID.</summary>
        public async Task DeleteColorAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
            }
        }

        // Materials

        /// <summary>Gets a material by ID.</summary>
        public async Task<Material?> GetMaterialAsync(int materialId)
        {
            return await _context.Materials.FindAsync(materialId);
        }

        /// <summary>Updates an existing material.</summary>
        public async Task UpdateMaterialAsync(Material material)
        {
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes a material by ID.</summary>
        public async Task DeleteMaterialAsync(int materialId)
        {
            var material = await _context.Materials.FindAsync(materialId);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }

        // Sketches

        /// <summary>Gets a sketch by ID.</summary>
        public async Task<Sketch?> GetSketchAsync(int sketchId)
        {
            return await _context.Sketches.FindAsync(sketchId);
        }

        /// <summary>Updates an existing sketch.</summary>
        public async Task UpdateSketchAsync(Sketch sketch)
        {
            _context.Sketches.Update(sketch);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes a sketch by ID.</summary>
        public async Task DeleteSketchAsync(int sketchId)
        {
            var sketch = await _context.Sketches.FindAsync(sketchId);
            if (sketch != null)
            {
                _context.Sketches.Remove(sketch);
                await _context.SaveChangesAsync();
            }
        }

        // Lookbook

        /// <summary>Fetches a lookbook image by ID.</summary>
        public async Task<LookbookImage?> GetLookbookImageAsync(int imageId)
        {
            return await _context.LookbookImages.FindAsync(imageId);
        }

        /// <summary>Adds a new lookbook image to a collection.</summary>
        public async Task AddLookbookImageAsync(int collectionId, LookbookImage image)
        {
            image.CollectionId = collectionId;
            _context.LookbookImages.Add(image);
            await _context.SaveChangesAsync();
        }

        /// <summary>Updates an existing lookbook image.</summary>
        public async Task UpdateLookbookImageAsync(LookbookImage image)
        {
            _context.LookbookImages.Update(image);
            await _context.SaveChangesAsync();
        }

        /// <summary>Deletes a lookbook image by ID.</summary>
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

