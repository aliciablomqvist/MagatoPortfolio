namespace Magato.Api.DTO;
public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public string Category { get; set; } = default!;
    public List<string> ImageUrls { get; set; } = new();
}
