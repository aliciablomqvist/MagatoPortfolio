using Magato.Api.DTO;

namespace Magato.Api.Services;

public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAll();
    CategoryDto? GetById(int id);
    void Add(CategoryDto dto);
    void Update(int id, CategoryDto dto);
    void Delete(int id);
}
