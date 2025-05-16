// <copyright file="SocialMediaLinkDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

public class SocialMediaLinkDto
{
    public string Platform{ get; set; } = default!;

    public string Url{ get; set; } = default!;
}
