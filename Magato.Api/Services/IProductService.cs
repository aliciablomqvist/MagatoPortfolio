using Magato.Api.DTO;

namespace Magato.Api.Services;
public interface IProductService
{
    IEnumerable<ProductDto> GetAll();
    ProductDto? Get(int id);
    void Add(ProductDto dto);
    void Update(ProductDto dto);
    void Delete(int id);
}
