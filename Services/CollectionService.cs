using ApiTest.DTO;
using ApiTest.Models;
using ApiTest.Repositories;

namespace ApiTest.Services
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
            return await _repo.GetAllAsync();
        }

        public async Task<Collection?> GetCollectionByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddCollectionAsync(CollectionDto dto)
        {
            var collection = new Collection { Name = dto.Name };
            await _repo.AddAsync(collection);
        }

        public async Task<bool> UpdateCollectionAsync(int id, CollectionDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCollectionAsync(int id)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists) return false;

            await _repo.DeleteAsync(id);
            return true;

        }
    }