using ApiTest.Models;

namespace ApiTest.Repositories
{
    public interface ICollectionRepository
    {
        Task<IEnumerable<Collection>> GetAllCollectionsAsync();
        Task<Collection?> GetCollectionByIdAsync(int id);
        Task AddCollectionAsync(Collection collection);
        Task UpdateCollectionAsync(Collection collection);
        Task DeleteCollectionAsync(int id);
        Task<bool> CollectionsExistsAsync(int id);

    }