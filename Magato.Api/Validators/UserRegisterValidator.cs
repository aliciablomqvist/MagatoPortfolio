// <copyright file="UserRegisterValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Validators;
using FluentValidation;

using Magato.Api.DTO;
public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        this.RuleFor(x => x.Username).NotEmpty();
        this.RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
