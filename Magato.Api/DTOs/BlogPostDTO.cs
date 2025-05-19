// <copyright file="BlogPostDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;
public class BlogPostDto
{
    public int Id
{
        get; set;
    }

    public string Title{ get; set; } = string.Empty;

    public string Content{ get; set; } = string.Empty;

    public string? Author
{
        get; set;
    }

    public DateTime PublishedAt
{
        get; set;
    }

    public string Slug{ get; set; } = string.Empty;

    public List<string> Tags{ get; set; } = new ();

    public List<string> ImageUrls{ get; set; } = new ();
}
