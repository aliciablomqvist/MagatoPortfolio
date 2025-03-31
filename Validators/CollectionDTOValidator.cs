using FluentValidation;
using Magato.Api..DTO;

namespace Magato.Api..Validators
{
    public class CollectionDtoValidator : AbstractValidator<CollectionDto>
    {
        public CollectionDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Namn på kollektion krävs.");
        }
    }
}
