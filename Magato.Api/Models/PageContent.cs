// <copyright file="PageContent.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Models;
public class PageContent
{
    public int Id
    {
        get; set;
    }

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

    public DateTime LastModified
    {
        get; set;
    }

    public List<string> ImageUrls { get; set; } = new();

    public List<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
}
