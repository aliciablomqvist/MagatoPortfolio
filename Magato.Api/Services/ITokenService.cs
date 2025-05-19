// <copyright file="ITokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.Models;
public interface ITokenService
{
    string GenerateToken(User user);
}
