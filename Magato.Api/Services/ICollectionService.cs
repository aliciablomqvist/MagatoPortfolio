using Magato.Api.DTO;
using Magato.Api.Models;

namespace Magato.Api.Services
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task<Collection> AddCollectionAsync(CollectionDto dto);

        Task<bool> UpdateCollectionAsync(int id, CollectionDto dto);
        Task<bool> DeleteCollectionAsync(int id);

        Task<bool> CollectionExistsAsync(int id);

        //Lookbook? Bilder?
        Task<bool> AddLookbookImageAsync(int collectionId, LookbookImageDto dto);
        Task<bool> UpdateLookbookImageAsync(int imageId, LookbookImageDto dto);
        Task<bool> DeleteLookbookImageAsync(int imageId);


        Task<bool> AddSketchAsync(int collectionId, SketchDto dto);
        Task<bool> AddMaterialAsync(int collectionId, MaterialDto dto);
        Task<bool> AddColorAsync(int collectionId, ColorDto dto);

        Task<bool> UpdateColorAsync(int colorId, ColorDto dto);
        Task<bool> DeleteColorAsync(int colorId);
        Task<bool> UpdateMaterialAsync(int materialId, MaterialDto dto);
        Task<bool> DeleteMaterialAsync(int materialId);
        Task<bool> UpdateSketchAsync(int sketchId, SketchDto dto);
        Task<bool> DeleteSketchAsync(int sketchId);
    }
}