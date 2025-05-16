// <copyright file="ProductImage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Models;

public class ProductImage
{
    public int Id
    {
        get; set;
    }

    public string ImageUrl { get; set; } = string.Empty;

    public int ProductId
    {
        get; set;
    }

    public Product Product { get; set; } = null!;
}
