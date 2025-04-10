using Magato.Api.Models;
using System.ComponentModel.DataAnnotations;


namespace Magato.Api.DTO;
public class ContactMessageDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    public bool GdprConsent { get; set; }
}
