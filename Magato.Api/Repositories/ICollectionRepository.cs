using Magato.Api.Models;

namespace Magato.Api.Repositories
{
    /// <summary>
    /// Interface for handling data access related to collections and their child entities.
    /// </summary>
    public interface ICollectionRepository
    {
        // Collections
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task AddCollectionAsync(Collection collection);
        Task UpdateCollectionAsync(Collection collection);
        Task DeleteCollectionAsync(int id);
        Task<bool> CollectionExistsAsync(int id);

        // Colors
        Task<ColorOption?> GetColorByIdAsync(int id);
        Task UpdateColorAsync(ColorOption color);
        Task DeleteColorAsync(int id);

        // Materials
        Task<Material?> GetMaterialAsync(int materialId);
        Task UpdateMaterialAsync(Material material);
        Task DeleteMaterialAsync(int materialId);

        // Sketches
        Task<Sketch?> GetSketchAsync(int sketchId);
        Task UpdateSketchAsync(Sketch sketch);
        Task DeleteSketchAsync(int sketchId);

        // Lookbook Images
        Task AddLookbookImageAsync(int collectionId, LookbookImage image);
        Task<LookbookImage?> GetLookbookImageAsync(int imageId);
        Task UpdateLookbookImageAsync(LookbookImage image);
        Task DeleteLookbookImageAsync(int imageId);
    }
}
