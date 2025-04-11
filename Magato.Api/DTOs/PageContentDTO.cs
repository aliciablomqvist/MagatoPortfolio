public class PageContentDto
{
    public string Key { get; set; } = null!;
    public string Title { get; set; } = string.Empty;

    public string? MainText { get; set; } = null;
    public string? SubText { get; set; } = null;
    public string? ExtraText { get; set; } = null;

    public bool Published { get; set; } = false;

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public List<string> MediaUrls { get; set; } = new();
}

