// <copyright file="ColorDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

/// <summary>
/// Represents a color option belonging to a collection.
/// </summary>
public class ColorDto
{
    public string Name { get; set; } = string.Empty;

    public string Hex { get; set; } = string.Empty;
}
