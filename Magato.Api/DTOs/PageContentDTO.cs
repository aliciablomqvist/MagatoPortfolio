namespace Magato.Api.DTO;
public class PageContentDto
{
    public string Key { get; set; } = null!;
    public string Title { get; set; } = string.Empty;

    public string? MainText
    {
        get; set;
    }
    public string? SubText
    {
        get; set;
    }
    public string? ExtraText
    {
        get; set;
    }

    public bool Published
    {
        get; set;
    }

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public List<string> MediaUrls { get; set; } = new();

    public List<SocialMediaLinkDto> SocialMediaLinks { get; set; } = new();
}
