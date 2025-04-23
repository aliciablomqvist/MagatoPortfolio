using Magato.Api.Models;

namespace Magato.Api.DTO;
public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

    public List<string> ImageUrls { get; set; } = new();

    public int CategoryId
    {
        get; set;
    }
    public string CategoryName { get; set; } = default!;


    public ProductStatus Status
    {
        get; set;
    }
}
