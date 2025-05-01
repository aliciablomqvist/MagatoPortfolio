using FluentValidation;

using Magato.Api.DTO;

public class LookbookImageDtoValidator : AbstractValidator<LookbookImageDto>
{
    public LookbookImageDtoValidator()
    {
        RuleFor(i => i.Url)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Invalid URL format.");

        RuleFor(i => i.Description)
            .MaximumLength(300).WithMessage("Description cannot exceed 300 characters.");
    }
}
