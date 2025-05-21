// <copyright file="UserLoginValidator.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;
public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        this.RuleFor(x => x.Username).NotEmpty();
        this.RuleFor(x => x.Password).NotEmpty();
    }
}
