// <copyright file="CategoryDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;

public class CategoryDto
{
    public int Id
    {
        get; set;
    }

    public string Name { get; set; } = default!;
}
