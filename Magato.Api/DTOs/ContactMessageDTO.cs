// <copyright file="ContactMessageDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

using Magato.Api.Models;

namespace Magato.Api.DTO;
public class ContactMessageDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    public bool GdprConsent
    {
        get; set;
    }

    // Honeypot
    public string? Honeypot
    {
        get; set;
    }
}
