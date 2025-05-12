// <copyright file="CollectionDTOValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using FluentValidation.AspNetCore;

using Magato.Api.DTO;

namespace Magato.Api.Validators
{
    public class CollectionDtoValidator : AbstractValidator<CollectionDto>
    {
        public CollectionDtoValidator()
        {
            this.RuleFor(x => x.CollectionTitle).NotEmpty().WithMessage("You must specify a title for the collections.");
        }
    }
}
