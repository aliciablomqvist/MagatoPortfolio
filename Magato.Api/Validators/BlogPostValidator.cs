// <copyright file="BlogPostValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;
using Magato.Api.DTO;

public class BlogPostValidator : AbstractValidator<BlogPostDto>
{
    public BlogPostValidator()
    {
        this.RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        this.RuleFor(x => x.Content).NotEmpty();
        this.RuleFor(x => x.Author).NotEmpty();
        this.RuleFor(x => x.PublishedAt).LessThanOrEqualTo(DateTime.UtcNow);
        this.RuleFor(x => x.Tags).NotNull();
        this.RuleForEach(x => x.Tags).NotEmpty();
        this.RuleFor(x => x.ImageUrls).NotNull();
        this.RuleForEach(x => x.ImageUrls).Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Each image URL must be a valid absolute URI.");
    }
}
