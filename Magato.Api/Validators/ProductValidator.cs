// <copyright file="ProductValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        this.RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.Description).NotEmpty();
        this.RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        // RuleFor(x => x.Category).NotEmpty(); kolla detta
        this.RuleForEach(x => x.ImageUrls).Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute));
        this.RuleFor(x => x.IsForSale).NotNull();
    }
}
