// <copyright file="IRefreshTokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

public interface IRefreshTokenService
{
    RefreshToken CreateAndStore(string username);

    RefreshToken? Get(string token);

    void Revoke(string token);
}
