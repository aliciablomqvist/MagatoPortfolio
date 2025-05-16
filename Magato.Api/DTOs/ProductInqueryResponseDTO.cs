// <copyright file="ProductInqueryResponseDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.DTO;

public class ProductInquiryResponseDto
{
    public int Id
    {
        get; set;
    }

    public string ProductTitle { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Size
    {
        get; set;
    }

    public bool IsHandled
    {
        get; set;
    }

    public DateTime SentAt
    {
        get; set;
    }
}
