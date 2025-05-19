// <copyright file="PageContentDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;
public class PageContentDto
{
    public string Key{ get; set; } = null!;

    public string Title{ get; set; } = string.Empty;

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

    public DateTime LastModified{ get; set; } = DateTime.UtcNow;

    public List<string> ImageUrls{ get; set; } = new ();

    public List<SocialMediaLinkDto> SocialMediaLinks{ get; set; } = new ();
}
