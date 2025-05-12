// <copyright file="ColorOption.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

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
