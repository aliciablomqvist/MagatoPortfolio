// <copyright file="IUserService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;

public interface IUserService
{
    User RegisterAdmin(UserRegisterDto dto);

    User Authenticate(UserLoginDto dto);

    User GetByUsername(string username);
}
