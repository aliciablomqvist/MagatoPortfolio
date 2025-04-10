using FluentValidation;
using Magato.Api.DTO;
using FluentValidation.AspNetCore;

namespace Magato.Api.Validators;

public class ContactMessageValidator //Kontrollerar bland annat format för namn, email osv
{
    public List<string> Validate(ContactMessageDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Namn krävs.");
        if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
            errors.Add("Ogiltig e-post.");
        if (string.IsNullOrWhiteSpace(dto.Message))
            errors.Add("Meddelande krävs.");

        return errors;
    }
}
