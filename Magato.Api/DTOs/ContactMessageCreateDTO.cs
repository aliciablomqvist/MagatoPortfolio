// <copyright file="ContactMessageCreateDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.DTO;
using System.ComponentModel.DataAnnotations;

public class ContactMessageCreateDto
{
    [Required]
    public string Name{ get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email{ get; set; } = string.Empty;

    [Required]
    public string Message{ get; set; } = string.Empty;

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
