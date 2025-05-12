// <copyright file="UserRegisterValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;

namespace Magato.Api.Validators;
public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        this.RuleFor(x => x.Username).NotEmpty();
        this.RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
