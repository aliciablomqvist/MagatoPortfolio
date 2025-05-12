// <copyright file="IUserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;

namespace Magato.Api.Services;
public interface IUserService
{
    User RegisterAdmin(UserRegisterDto dto);

    User Authenticate(UserLoginDto dto);

    User GetByUsername(string username);
}
