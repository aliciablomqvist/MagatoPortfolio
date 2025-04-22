using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;

public class ProductImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
