// <copyright file="SketchDTOValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
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
