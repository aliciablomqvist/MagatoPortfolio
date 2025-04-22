using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }


    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();


    public ICollection<ProductInquiry> ProductInquiries { get; set; } = new List<ProductInquiry>();

    public enum StockStatus
    {
        InStock,
        PreOrder,
        OutOfStock
    }

    public StockStatus Status
    {
        get; set;
    }


}
