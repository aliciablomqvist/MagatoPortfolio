// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Models;

public class Product
{
    public int Id
    {
        get; set;
    }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price
    {
        get; set;
    }

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public ICollection<ProductInquiry> ProductInquiries { get; set; } = new List<ProductInquiry>();

    public ProductStatus Status
    {
        get; set;
    }

    public int CategoryId
    {
        get; set;
    }

    public Category Category { get; set; } = default!;

    public bool IsForSale { get; set; } = false;

    public int? CollectionId
    {
        get; set;
    }
    public Collection? Collection { get; set; } = null!;
}
