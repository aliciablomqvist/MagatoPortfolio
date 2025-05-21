// <copyright file="ITokenService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.Models;
public interface ITokenService
{
    string GenerateToken(User user);
}
