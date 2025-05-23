// <copyright file="ContactMessageValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class ContactMessageValidator : AbstractValidator<ContactMessageCreateDto>
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

    public List<string> ValidateAndExtractErrors(ContactMessageCreateDto dto)
    {
        var result = this.Validate(dto);
        return result.IsValid
            ? new List<string>()
            : result.Errors.Select(e => e.ErrorMessage).ToList();
    }
}
