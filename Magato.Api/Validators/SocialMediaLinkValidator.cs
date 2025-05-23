// <copyright file="SocialMediaLinkValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class SocialMediaLinkDtoValidator : AbstractValidator<SocialMediaLinkDto>
{
    public SocialMediaLinkDtoValidator()
    {
        this.RuleFor(link => link.Platform)
            .NotEmpty().WithMessage("Platform is required.")
            .MaximumLength(50);

        this.RuleFor(link => link.Url)
            .NotEmpty().WithMessage("URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Invalid URL format.");
    }
}
