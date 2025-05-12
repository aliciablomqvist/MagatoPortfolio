using FluentValidation;

using Magato.Api.DTO;

namespace ApiTest.Validators
{
    public class ColorDtoValidator : AbstractValidator<ColorDto>
    {
        public ColorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Hex)
                .NotEmpty()
                .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
                .WithMessage("Have to be in the following format: #RRGGBB.");
        }
    }
}
