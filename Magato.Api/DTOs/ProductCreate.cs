// <copyright file="ProductCreate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.DTO;
using Magato.Api.Models;

// Hur används denna?
public class ProductCreateDto
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price
    {
        get; set;
    }

    public int CategoryId
    {
        get; set;
    }

    public List<string> ImageUrls { get; set; } = new ();

    public ProductStatus Status
    {
        get; set;
    }
}
