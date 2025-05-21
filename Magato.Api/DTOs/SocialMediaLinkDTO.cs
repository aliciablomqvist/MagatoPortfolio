// <copyright file="SocialMediaLinkDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

public class SocialMediaLinkDto
{
    public string Platform { get; set; } = default!;

    public string Url { get; set; } = default!;
}
