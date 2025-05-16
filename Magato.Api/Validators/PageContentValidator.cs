// <copyright file="PageContentValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class PageContentValidator : AbstractValidator<PageContentDto>
{
    public PageContentValidator()
    {
        this.RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key måste anges.")
            .MaximumLength(100).WithMessage("Key has to be max 100 characters long");

        this.RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel måste anges.")
            .MaximumLength(200).WithMessage("Title has to be max 200 characters long");

        this.RuleFor(x => x.MainText)
            .NotEmpty().WithMessage("MainText cannot be empty.");

        this.RuleForEach(x => x.ImageUrls)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Has to be a valid URL.");

        this.RuleFor(x => x.LastModified)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("LastModified cannot be in the future.");
    }
}
