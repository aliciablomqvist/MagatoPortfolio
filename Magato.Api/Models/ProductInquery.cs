using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;
public class ProductInquiry
{
    public int Id
    {
        get; set;
    }

    public int ProductId
    {
        get; set;
    }
    public Product Product { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}

