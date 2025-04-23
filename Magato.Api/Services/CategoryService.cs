using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<CategoryDto> GetAll()
        => _repo.GetAll().Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });

    public void Add(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _repo.Add(category);
    }
}
