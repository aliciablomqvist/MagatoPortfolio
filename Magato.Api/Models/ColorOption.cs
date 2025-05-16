// <copyright file="ColorOption.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Models;

using System.Text.Json.Serialization;

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
