// <copyright file="ColorDTOValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace ApiTest.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class ColorDtoValidator : AbstractValidator<ColorDto>
{
    public ColorDtoValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
        this.RuleFor(x => x.Hex)
            .NotEmpty()
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("Have to be in the following format: #RRGGBB.");
    }
}
