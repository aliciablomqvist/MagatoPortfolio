using FluentValidation;
using Magato.Api.DTO;
using FluentValidation.AspNetCore;

namespace Magato.Api.Validators;
public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}