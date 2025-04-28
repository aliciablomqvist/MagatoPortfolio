using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class PageContentValidator : AbstractValidator<PageContentDto>
{
    public PageContentValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key måste anges.")
            .MaximumLength(100).WithMessage("Key has to be max 100 characters long");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel måste anges.")
            .MaximumLength(200).WithMessage("Title has to be max 200 characters long");

        RuleFor(x => x.MainText)
            .NotEmpty().WithMessage("MainText cannot be empty.");

        RuleForEach(x => x.MediaUrls)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Has to be a valid URL.");

        RuleFor(x => x.LastModified)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("LastModified cannot be in the future.");
    }
}
