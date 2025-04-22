using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<ProductDto> GetAll()
        => _repo.GetAll().Select(Map);

    public ProductDto? Get(int id)
    {
        var product = _repo.Get(id);
        return product == null ? null : Map(product);
    }

    public void Add(ProductDto dto)
        => _repo.Add(Map(dto));

    public void Update(ProductDto dto)
        => _repo.Update(Map(dto));

    public void Delete(int id)
        => _repo.Delete(id);

    private static ProductDto Map(Product p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        Price = p.Price,
        ImageUrls = p.ProductImages.Select(i => i.ImageUrl).ToList()
    };

    private static Product Map(ProductDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        ProductImages = dto.ImageUrls.Select(url => new ProductImage { ImageUrl = url }).ToList()
    };
}
