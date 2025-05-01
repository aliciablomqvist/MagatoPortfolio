using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class ContactMessageValidator : AbstractValidator<ContactMessageDto>
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public ContactMessageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegex).WithMessage("Invalid email format.")
            .MaximumLength(200);

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(1000);

        RuleFor(x => x.GdprConsent)
            .Equal(true).WithMessage("You must accept GDPR terms.");

        // Honeypot
        RuleFor(x => x.Honeypot)
            .Must(string.IsNullOrWhiteSpace)
            .WithMessage("Potential spam detected.");

    }
    public List<string> ValidateAndExtractErrors(ContactMessageDto dto)
    {
        var result = base.Validate(dto);
        return result.IsValid
            ? new List<string>()
            : result.Errors.Select(e => e.ErrorMessage).ToList();
    }

}
