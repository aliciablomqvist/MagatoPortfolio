// <copyright file="Category.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Models;

public class Category
{
    public int Id
    {
        get; set;
    }

    public string Name { get; set; } = default!;

    public List<Product> Products { get; set; } = new();
}
