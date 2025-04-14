using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Category).NotEmpty();
        RuleForEach(x => x.ImageUrls).Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute));
    }
}
