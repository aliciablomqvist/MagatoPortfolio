using FluentValidation;
using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class BlogPostValidator : AbstractValidator<BlogPostDto>
{
    public BlogPostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .NotEmpty();

        RuleFor(x => x.PublishedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("PublishedAt cannot be in the future.");

        RuleFor(x => x.Slug)
            .NotEmpty()
            .MaximumLength(100)
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$")
            .WithMessage("Slug must be lowercase and may only contain letters, numbers, and hyphens.");

        RuleFor(x => x.Tags)
            .NotNull()
            .Must(tags => tags.All(tag => !string.IsNullOrWhiteSpace(tag)))
            .WithMessage("Tags cannot contain empty values.");

        RuleFor(x => x.ImageUrls)
            .NotNull()
            .Must(urls => urls.All(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)))
            .WithMessage("All image URLs must be valid.");
    }
}
