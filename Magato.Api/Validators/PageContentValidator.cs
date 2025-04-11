using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;

public class PageContentValidator : AbstractValidator<PageContentDto>
{
    public PageContentValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key måste anges.")
            .MaximumLength(100).WithMessage("Key får max vara 100 tecken.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Titel måste anges.")
            .MaximumLength(200).WithMessage("Titel får max vara 200 tecken.");

        RuleFor(x => x.MainText)
            .NotEmpty().WithMessage("MainText kan inte vara tom.");

        RuleForEach(x => x.MediaUrls)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("Alla media-URL:er måste vara giltiga URL:er.");

        RuleFor(x => x.LastModified)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("LastModified får inte vara i framtiden.");
    }
}
