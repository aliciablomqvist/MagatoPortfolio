using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;

public class SocialMediaLink
{
    public int Id
    {
        get; set;
    }
    public string Platform { get; set; } = default!;
    public string Url { get; set; } = default!;
}
