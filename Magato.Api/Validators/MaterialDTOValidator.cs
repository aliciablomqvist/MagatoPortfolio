// <copyright file="MaterialDTOValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;

using Magato.Api.DTO;

public class MaterialDtoValidator : AbstractValidator<MaterialDto>
{
    public MaterialDtoValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
        this.RuleFor(x => x.Description).NotEmpty();
    }
}
