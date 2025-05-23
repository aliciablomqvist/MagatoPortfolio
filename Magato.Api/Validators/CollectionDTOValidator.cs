// <copyright file="CollectionDTOValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

using Magato.Api.DTO;

public class CollectionDtoValidator : AbstractValidator<CollectionDto>
{
    public CollectionDtoValidator()
    {
        this.RuleFor(x => x.CollectionTitle).NotEmpty().WithMessage("You must specify a title for the collections.");
    }
}
