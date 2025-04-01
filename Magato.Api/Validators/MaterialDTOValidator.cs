using FluentValidation;
using Magato.Api.DTO;

public class MaterialDtoValidator : AbstractValidator<MaterialDto>
{
    public MaterialDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
