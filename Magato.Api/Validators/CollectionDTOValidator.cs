using FluentValidation;
using Magato.Api.DTO;
using FluentValidation.AspNetCore;

namespace Magato.Api.Validators
{
    public class CollectionDtoValidator : AbstractValidator<CollectionDto>
    {
        public CollectionDtoValidator()
        {
            RuleFor(x => x.CollectionTitle).NotEmpty().WithMessage("You must specify a title for the collections.");
        }
    }
}
