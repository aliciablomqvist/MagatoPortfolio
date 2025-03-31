using Magato.Api..DTO;
using Magato.Api..Models;
using Magato.Api..Repositories;

namespace Magato.Api..Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _repo;

        public CollectionService(ICollectionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            return await _repo.GetAllCollectionsAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _repo.GetCollectionByIdAsync(id);
        }

        public async Task AddCollectionAsync(CollectionDto dto)
        {
            var collection = new Collection { Name = dto.Name };
            await _repo.AddCollectionAsync(collection);
        }

        public async Task<bool> UpdateCollectionAsync(int id, CollectionDto dto)
        {
            var existing = await _repo.GetCollectionByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            await _repo.UpdateCollectionAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var exists = await _repo.CollectionExistsAsync(id);
            if (!exists) return false;

            await _repo.DeleteCollectionAsync(id);
            return true;

        }
    }