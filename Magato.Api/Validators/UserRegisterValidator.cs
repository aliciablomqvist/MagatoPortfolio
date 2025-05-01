using FluentValidation;

namespace Magato.Api.Validators;
public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
        public UserRegisterValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
