using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    void Add(Category category);
}
