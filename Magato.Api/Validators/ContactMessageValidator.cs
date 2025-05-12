// <copyright file="ContactMessageValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class ContactMessageValidator : AbstractValidator<ContactMessageDto>
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public ContactMessageValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        this.RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegex).WithMessage("Invalid email format.")
            .MaximumLength(200);

        this.RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .MaximumLength(1000);

        this.RuleFor(x => x.GdprConsent)
            .Equal(true).WithMessage("You must accept GDPR terms.");

        // Honeypot
        this.RuleFor(x => x.Honeypot)
            .Must(string.IsNullOrWhiteSpace)
            .WithMessage("Potential spam detected.");
    }

    public List<string> ValidateAndExtractErrors(ContactMessageDto dto)
    {
        var result = this.Validate(dto);
        return result.IsValid
            ? new List<string>()
            : result.Errors.Select(e => e.ErrorMessage).ToList();
    }
}
