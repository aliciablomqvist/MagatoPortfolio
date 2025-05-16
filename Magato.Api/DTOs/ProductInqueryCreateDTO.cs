// <copyright file="ProductInqueryCreateDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO;
public class ProductInquiryCreateDto
{
    public int ProductId
    {
        get; set;
    }

    public string Email { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Size
    {
        get; set;
    }

    // Honeypot
    public string? Honeypot
    {
        get; set;
    }
}
