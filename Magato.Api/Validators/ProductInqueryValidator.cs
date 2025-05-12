// <copyright file="ProductInqueryValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class ProductInquiryValidator : AbstractValidator<ProductInquiryDto>
{
    private const string EmailRegex =
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public ProductInquiryValidator()
    {
        this.RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Invalid product.");

        this.RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegex).WithMessage("Invalid email format.")
            .MaximumLength(200);
        this.RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Message is too long.");

        this.RuleFor(x => x.Size)
            .MaximumLength(20)
            .WithMessage("Size input is too long.");

        // Honeypot för spam
        this.RuleFor(x => x.Honeypot).Must(string.IsNullOrWhiteSpace)
.WithMessage("Potential spam detected.");
    }
}
