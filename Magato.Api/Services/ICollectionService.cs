using Magato.Api.DTO;
using Magato.Api.Models;

namespace Magato.Api.Services
{
    /// <summary>
    /// Service interface for managing collections and their related data.
    /// </summary>
    /// 
    /// //NOTE: Ganska stort interface, bryta ner i midre delar?/// 
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task<Collection> AddCollectionAsync(CollectionCreateDto dto);
        Task<bool> UpdateCollectionAsync(int id, CollectionDto dto);
        Task<bool> DeleteCollectionAsync(int id);
        Task<bool> CollectionExistsAsync(int id);

        // Lookbook
        Task<bool> AddLookbookImageAsync(int collectionId, LookbookImageDto dto);
        Task<bool> UpdateLookbookImageAsync(int imageId, LookbookImageDto dto);
        Task<bool> DeleteLookbookImageAsync(int imageId);

        // Sketches
        Task<bool> AddSketchAsync(int collectionId, SketchDto dto);
        Task<bool> UpdateSketchAsync(int sketchId, SketchDto dto);
        Task<bool> DeleteSketchAsync(int sketchId);

        // Materials
        Task<bool> AddMaterialAsync(int collectionId, MaterialDto dto);
        Task<bool> UpdateMaterialAsync(int materialId, MaterialDto dto);
        Task<bool> DeleteMaterialAsync(int materialId);

        // Colors
        Task<bool> AddColorAsync(int collectionId, ColorDto dto);
        Task<bool> UpdateColorAsync(int colorId, ColorDto dto);
        Task<bool> DeleteColorAsync(int colorId);
    }
}