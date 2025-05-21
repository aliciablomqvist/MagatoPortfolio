// <copyright file="Material.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
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
