// <copyright file="BlogPost.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Models;
public class BlogPost
{
    public int Id
    {
        get; set;
    }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string? Author
    {
        get; set;
    }

    public DateTime PublishedAt
    {
        get; set;
    }

    public string Slug { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();

    public List<string> ImageUrls { get; set; } = new();
}
