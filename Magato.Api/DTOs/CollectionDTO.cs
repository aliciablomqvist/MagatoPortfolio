// <copyright file="CollectionDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

/// <summary>
/// Represents a collection with full details.
/// </summary>
public class CollectionDto
{
    public int Id
    {
        get; set;
    }

    public string CollectionTitle { get; set; } = string.Empty;

    public string CollectionDescription { get; set; } = string.Empty;

    public DateTime ReleaseDate
    {
        get; set;
    }

    public List<ColorDto> Colors { get; set; } = new();

    public List<MaterialDto> Materials { get; set; } = new();

    public List<SketchDto> Sketches { get; set; } = new();

    public List<LookbookImageDto> LookbookImages { get; set; } = new();
}
