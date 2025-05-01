using FluentValidation;

using Magato.Api.DTO;

public class SocialMediaLinkDtoValidator : AbstractValidator<SocialMediaLinkDto>
{
    public SocialMediaLinkDtoValidator()
    {
        RuleFor(link => link.Platform)
            .NotEmpty().WithMessage("Platform is required.")
            .MaximumLength(50);

        RuleFor(link => link.Url)
            .NotEmpty().WithMessage("URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Invalid URL format.");
    }
}
