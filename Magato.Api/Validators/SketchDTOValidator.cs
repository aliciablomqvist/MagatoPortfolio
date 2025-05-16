// <copyright file="SketchDTOValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class SketchDtoValidator : AbstractValidator<SketchDto>
{
    public SketchDtoValidator()
    {
        this.RuleFor(x => x.Url).NotEmpty().Must(u => u.StartsWith("http"))
            .WithMessage("Url has to start with http/https.");
    }
}
