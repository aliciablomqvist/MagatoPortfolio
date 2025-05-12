using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;
public class ContactMessage
{
    public int Id
    {
        get; set;
    }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool GdprConsent { get; set; } = false;
}
