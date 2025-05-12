// <copyright file="UserLoginValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using FluentValidation.AspNetCore;

using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        this.RuleFor(x => x.Username).NotEmpty();
        this.RuleFor(x => x.Password).NotEmpty();
    }
}
