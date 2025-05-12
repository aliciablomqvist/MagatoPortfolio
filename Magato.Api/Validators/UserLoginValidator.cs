using FluentValidation;
using FluentValidation.AspNetCore;

using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
