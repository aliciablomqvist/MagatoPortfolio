// <copyright file="ProductInquery.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Models;
public class ProductInquiry
{
    public int Id
    {
        get; set;
    }

    public string Email { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Size
    {
        get; set;
    }

    public bool IsHandled { get; set; } = false;

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public int ProductId
    {
        get; set;
    }

    public Product Product { get; set; } = null!;

    public ProductStatus Status
    {
        get; set;
    }
}
