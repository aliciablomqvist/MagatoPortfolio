// <copyright file="SocialMediaLink.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

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
