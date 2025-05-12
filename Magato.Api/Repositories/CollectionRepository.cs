// <copyright file="CollectionRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        private readonly ApplicationDbContext context;

        public CollectionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns all collections including related data (colors, materials, sketches, lookbook images).
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await this.context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection by ID including related entities.
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await this.context.Collections
                .Include(c => c.Colors)
                .Include(c => c.Materials)
                .Include(c => c.Sketches)
                .Include(c => c.LookbookImages)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Adds a new collection to the database.
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task AddCollectionAsync(Collection collection)
        {
            this.context.Collections.Add(collection);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing collection in the database.
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateCollectionAsync(Collection collection)
        {
            this.context.Collections.Update(collection);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a collection by ID if it exists.
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task DeleteCollectionAsync(int id)
        {
            var collection = await this.context.Collections.FindAsync(id);
            if (collection != null)
            {
                this.context.Collections.Remove(collection);
                await this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks whether a collection with the specified ID exists.
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<bool> CollectionExistsAsync(int id)
        {
            return await this.context.Collections.AnyAsync(c => c.Id == id);
        }

        // Colors

        /// <summary>Fetches a color by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<ColorOption?> GetColorByIdAsync(int id)
        {
            return await this.context.Colors.FindAsync(id);
        }

        /// <summary>Updates an existing color.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateColorAsync(ColorOption color)
        {
            this.context.Colors.Update(color);
            await this.context.SaveChangesAsync();
        }

        /// <summary>Deletes a color by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task DeleteColorAsync(int id)
        {
            var color = await this.context.Colors.FindAsync(id);
            if (color != null)
            {
                this.context.Colors.Remove(color);
                await this.context.SaveChangesAsync();
            }
        }

        // Materials

        /// <summary>Gets a material by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<Material?> GetMaterialAsync(int materialId)
        {
            return await this.context.Materials.FindAsync(materialId);
        }

        /// <summary>Updates an existing material.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateMaterialAsync(Material material)
        {
            this.context.Materials.Update(material);
            await this.context.SaveChangesAsync();
        }

        /// <summary>Deletes a material by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task DeleteMaterialAsync(int materialId)
        {
            var material = await this.context.Materials.FindAsync(materialId);
            if (material != null)
            {
                this.context.Materials.Remove(material);
                await this.context.SaveChangesAsync();
            }
        }

        // Sketches

        /// <summary>Gets a sketch by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<Sketch?> GetSketchAsync(int sketchId)
        {
            return await this.context.Sketches.FindAsync(sketchId);
        }

        /// <summary>Updates an existing sketch.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateSketchAsync(Sketch sketch)
        {
            this.context.Sketches.Update(sketch);
            await this.context.SaveChangesAsync();
        }

        /// <summary>Deletes a sketch by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task DeleteSketchAsync(int sketchId)
        {
            var sketch = await this.context.Sketches.FindAsync(sketchId);
            if (sketch != null)
            {
                this.context.Sketches.Remove(sketch);
                await this.context.SaveChangesAsync();
            }
        }

        // Lookbook

        /// <summary>Fetches a lookbook image by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<LookbookImage?> GetLookbookImageAsync(int imageId)
        {
            return await this.context.LookbookImages.FindAsync(imageId);
        }

        /// <summary>Adds a new lookbook image to a collection.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task AddLookbookImageAsync(int collectionId, LookbookImage image)
        {
            image.CollectionId = collectionId;
            this.context.LookbookImages.Add(image);
            await this.context.SaveChangesAsync();
        }

        /// <summary>Updates an existing lookbook image.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UpdateLookbookImageAsync(LookbookImage image)
        {
            this.context.LookbookImages.Update(image);
            await this.context.SaveChangesAsync();
        }

        /// <summary>Deletes a lookbook image by ID.</summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task DeleteLookbookImageAsync(int imageId)
        {
            var image = await this.context.LookbookImages.FindAsync(imageId);
            if (image != null)
            {
                this.context.LookbookImages.Remove(image);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
