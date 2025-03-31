using FluentValidation;
using Magato.Api.DTO;

public class SketchDtoValidator : AbstractValidator<SketchDto>
{
    public SketchDtoValidator()
    {
        RuleFor(x => x.Url).NotEmpty().Must(u => u.StartsWith("http"))
            .WithMessage("Url måste börja på http/https.");
    }
}
