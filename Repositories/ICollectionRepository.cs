using Magato.Api.Models;

namespace Magato.Api.Repositories
{
    public interface ICollectionRepository
    {
        //Collections
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

        // Material
        Task<Material?> GetMaterialAsync(int materialId);
        Task UpdateMaterialAsync(Material material);
        Task DeleteMaterialAsync(int materialId);

        // Sketch
        Task<Sketch?> GetSketchAsync(int sketchId);
        Task UpdateSketchAsync(Sketch sketch);
        Task DeleteSketchAsync(int sketchId);

    }
}