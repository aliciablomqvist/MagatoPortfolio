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

    public CategoryDto? GetById(int id)
    {
        var category = _repo.GetById(id);
        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public void Add(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _repo.Add(category);
    }

    public void Update(int id, CategoryDto dto)
    {
        var category = _repo.GetById(id);
        if (category == null)
            return;

        category.Name = dto.Name;
        _repo.Update(category);
    }

    public void Delete(int id)
    {
        var category = _repo.GetById(id);
        if (category == null)
            return;

        _repo.Delete(category);
    }
}
