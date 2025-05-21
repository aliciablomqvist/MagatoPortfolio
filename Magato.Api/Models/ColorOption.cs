// <copyright file="ColorOption.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Models;

public class ColorOption
{
    public int Id
    {
        get; set;
    }

    public required string Name
    {
        get; set;
    }

    public required string Hex
    {
        get; set;
    }

    public int CollectionId
    {
        get; set;
    }

    // Koppling till collection
    [JsonIgnore]
    public Collection? Collection
    {
        get; set;
    }
}
