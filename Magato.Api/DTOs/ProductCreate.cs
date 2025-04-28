using Magato.Api.Models;

namespace Magato.Api.DTO;

public class ProductCreateDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price
    {
        get; set;
    }
    public int CategoryId
    {
        get; set;
    }
    public List<string> ImageUrls { get; set; } = new();
    public ProductStatus Status
    {
        get; set;
    }
}
