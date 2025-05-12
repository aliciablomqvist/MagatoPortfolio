using System.Text.Json.Serialization;

using Magato.Api.Models;

namespace Magato.Api.DTO;
public class ProductDto
{
    public int Id
    {
        get; set;
    }
    public string Title { get; set; } = default!;

    public decimal Price
    {
        get; set;
    }
    public string Description { get; set; } = default!;

    public int CategoryId
    {
        get; set;
    }
    public string? CategoryName
    {
        get; set;
    }


    public List<string> ImageUrls { get; set; } = new();



    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductStatus Status
    {
        get; set;
    }
}
