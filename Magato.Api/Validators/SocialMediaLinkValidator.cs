// <copyright file="SocialMediaLinkValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
