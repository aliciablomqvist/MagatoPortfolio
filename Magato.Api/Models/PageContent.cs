// <copyright file="PageContent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;

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

    public List<string> ImageUrls { get; set; } = new ();

    public List<SocialMediaLink> SocialMediaLinks { get; set; } = new List<SocialMediaLink>();
}
