using FluentValidation;
using Magato.Api.DTO;
namespace Magato.Api.Validators;

    public class BlogPostValidator : AbstractValidator<BlogPostDto>
    {
        public BlogPostValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.PublishedAt).LessThanOrEqualTo(DateTime.UtcNow);
        }
    }
