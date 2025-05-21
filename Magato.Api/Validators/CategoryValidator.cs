// <copyright file="CategoryValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        this.RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}
