using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    Category? GetById(int id);
    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}
