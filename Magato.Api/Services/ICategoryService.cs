using Magato.Api.DTO;

namespace Magato.Api.Services;

public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAll();
    void Add(CategoryDto dto);
}
