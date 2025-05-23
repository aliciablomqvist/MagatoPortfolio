// <copyright file="IRefreshTokenService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
using Magato.Api.Models;
public interface IRefreshTokenService
{
    RefreshToken CreateAndStore(string username);

    RefreshToken? Get(string token);

    void Revoke(string token);
}
