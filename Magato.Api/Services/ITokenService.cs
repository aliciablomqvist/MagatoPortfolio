// <copyright file="ITokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Magato.Api.DTO;
using Magato.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Magato.Api.Services;
public interface ITokenService
{
    string GenerateToken(User user);
}
