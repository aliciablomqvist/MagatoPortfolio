using ApiTest.DTO;
using ApiTest.Models;

namespace ApiTest.Services
{
    public interface ICollectionService
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task AddCollectionAsync(CollectionDto dto);
        Task<bool> UpdateCollectionAsync(int id, CollectionDto dto);
        Task<bool> DeleteCollectionAsync(int id);

        //Likadant för materal, sketch, osv
    }
}