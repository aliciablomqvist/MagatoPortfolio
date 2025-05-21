// <copyright file="MaterialDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

/// <summary>
/// Represents a material used in the collection.
/// </summary>
public class MaterialDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // public string Url{ get; set; } = string.Empty;
}
