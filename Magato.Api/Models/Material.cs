// <copyright file="Material.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;
public class Material
{
    public int Id
    {
        get; set;
    }

    public required string Name
    {
        get; set;
    }

    public required string Description
    {
        get; set;
    }

    public int CollectionId
    {
        get; set;
    }

    [JsonIgnore]
    public Collection? Collection
    {
        get; set;
    }
}
