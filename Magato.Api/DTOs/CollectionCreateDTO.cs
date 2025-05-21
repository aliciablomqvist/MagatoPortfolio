// <copyright file="CollectionCreateDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

/// <summary>
/// Represents the data required to create a new collection.
/// </summary>
public class CollectionCreateDto
{
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
