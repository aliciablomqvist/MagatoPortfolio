// <copyright file="CollectionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services
{
    /// <summary>
    /// Service layer that handles business logic for collections and their related entities.
    /// </summary>
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository repo;

        public CollectionService(ICollectionRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await this.repo.GetAllCollectionsAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await this.repo.GetCollectionByIdAsync(id);
        }

        public async Task<Collection> AddCollectionAsync(CollectionCreateDto dto)
        {
            var collection = new Collection
            {
                CollectionTitle = dto.CollectionTitle,
                CollectionDescription = dto.CollectionDescription,
                ReleaseDate = dto.ReleaseDate,
                Colors = dto.Colors.Select(c => new ColorOption
                {
                    Name = c.Name,
                    Hex = c.Hex,
                }).ToList(),
                Materials = dto.Materials.Select(m => new Material
                {
                    Name = m.Name,
                    Description = m.Description,
                }).ToList(),
                Sketches = dto.Sketches.Select(s => new Sketch
                {
                    Url = s.Url
                }).ToList(),
            };

            await this.repo.AddCollectionAsync(collection);
            return collection;
        }

        public async Task<bool> UpdateCollectionAsync(int id, CollectionDto dto)
        {
            var existing = await this.repo.GetCollectionByIdAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.CollectionTitle = dto.CollectionTitle;
            existing.CollectionDescription = dto.CollectionDescription;
            existing.ReleaseDate = dto.ReleaseDate;

            existing.Colors = dto.Colors.Select(c => new ColorOption { Name = c.Name, Hex = c.Hex }).ToList();
            existing.Materials = dto.Materials.Select(m => new Material { Name = m.Name, Description = m.Description }).ToList();
            existing.Sketches = dto.Sketches.Select(s => new Sketch { Url = s.Url }).ToList();

            await this.repo.UpdateCollectionAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var exists = await this.repo.CollectionExistsAsync(id);
            if (!exists)
            {
                return false;
            }

            await this.repo.DeleteCollectionAsync(id);
            return true;
        }

        public async Task<bool> CollectionExistsAsync(int id)
        {
            return await this.repo.CollectionExistsAsync(id);
        }

        public async Task<bool> AddSketchAsync(int collectionId, SketchDto dto)
        {
            var collection = await this.repo.GetCollectionByIdAsync(collectionId);
            if (collection == null)
            {
                return false;
            }

            collection.Sketches.Add(new Sketch
            {
                Url = dto.Url,
                CollectionId = collectionId,
            });

            await this.repo.UpdateCollectionAsync(collection);
            return true;
        }

        public async Task<bool> AddMaterialAsync(int collectionId, MaterialDto dto)
        {
            var collection = await this.repo.GetCollectionByIdAsync(collectionId);
            if (collection == null)
            {
                return false;
            }

            collection.Materials.Add(new Material
            {
                Name = dto.Name,
                Description = dto.Description,
                CollectionId = collectionId,
            });

            await this.repo.UpdateCollectionAsync(collection);
            return true;
        }

        public async Task<bool> AddColorAsync(int collectionId, ColorDto dto)
        {
            var collection = await this.repo.GetCollectionByIdAsync(collectionId);
            if (collection == null)
            {
                return false;
            }

            collection.Colors.Add(new ColorOption
            {
                Name = dto.Name,
                Hex = dto.Hex,
                CollectionId = collectionId,
            });

            await this.repo.UpdateCollectionAsync(collection);
            return true;
        }

        public async Task<bool> UpdateColorAsync(int colorId, ColorDto dto)
        {
            var color = await this.repo.GetColorByIdAsync(colorId);
            if (color == null)
            {
                return false;
            }

            color.Name = dto.Name;
            color.Hex = dto.Hex;

            await this.repo.UpdateColorAsync(color);
            return true;
        }

        public async Task<bool> DeleteColorAsync(int colorId)
        {
            var color = await this.repo.GetColorByIdAsync(colorId);
            if (color == null)
            {
                return false;
            }

            await this.repo.DeleteColorAsync(colorId);
            return true;
        }

        public async Task<bool> UpdateMaterialAsync(int materialId, MaterialDto dto)
        {
            var material = await this.repo.GetMaterialAsync(materialId);
            if (material == null)
            {
                return false;
            }

            material.Name = dto.Name;
            material.Description = dto.Description;

            await this.repo.UpdateMaterialAsync(material);
            return true;
        }

        public async Task<bool> DeleteMaterialAsync(int materialId)
        {
            var material = await this.repo.GetMaterialAsync(materialId);
            if (material == null)
            {
                return false;
            }

            await this.repo.DeleteMaterialAsync(materialId);
            return true;
        }

        public async Task<bool> UpdateSketchAsync(int sketchId, SketchDto dto)
        {
            var sketch = await this.repo.GetSketchAsync(sketchId);
            if (sketch == null)
            {
                return false;
            }

            sketch.Url = dto.Url;

            await this.repo.UpdateSketchAsync(sketch);
            return true;
        }

        public async Task<bool> DeleteSketchAsync(int sketchId)
        {
            var sketch = await this.repo.GetSketchAsync(sketchId);
            if (sketch == null)
            {
                return false;
            }

            await this.repo.DeleteSketchAsync(sketchId);
            return true;
        }

        public async Task<bool> AddLookbookImageAsync(int collectionId, LookbookImageDto dto)
        {
            var collection = await this.repo.GetCollectionByIdAsync(collectionId);
            if (collection == null)
            {
                return false;
            }

            var image = new LookbookImage
            {
                Url = dto.Url,
                Description = dto.Description,
                CollectionId = collectionId,
            };

            await this.repo.AddLookbookImageAsync(collectionId, image);
            return true;
        }

        public async Task<bool> UpdateLookbookImageAsync(int imageId, LookbookImageDto dto)
        {
            var existing = await this.repo.GetLookbookImageAsync(imageId);
            if (existing == null)
            {
                return false;
            }

            existing.Url = dto.Url;
            existing.Description = dto.Description;

            await this.repo.UpdateLookbookImageAsync(existing);
            return true;
        }

        public async Task<bool> DeleteLookbookImageAsync(int imageId)
        {
            var existing = await this.repo.GetLookbookImageAsync(imageId);
            if (existing == null)
            {
                return false;
            }

            await this.repo.DeleteLookbookImageAsync(imageId);
            return true;
        }
    }
}
