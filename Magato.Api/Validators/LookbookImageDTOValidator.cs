// <copyright file="LookbookImageDTOValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class LookbookImageDtoValidator : AbstractValidator<LookbookImageDto>
{
    public LookbookImageDtoValidator()
    {
        this.RuleFor(i => i.Url)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Invalid URL format.");

        this.RuleFor(i => i.Description)
            .MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");
    }
}
