using FluentValidation;
using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class BlogPostValidator : AbstractValidator<BlogPostDto>
{
    public BlogPostValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Author).NotEmpty();
        RuleFor(x => x.PublishedAt).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Tags).NotNull();
        RuleForEach(x => x.Tags).NotEmpty();
        RuleFor(x => x.ImageUrls).NotNull();
        RuleForEach(x => x.ImageUrls).Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Each image URL must be a valid absolute URI.");
    }
}
