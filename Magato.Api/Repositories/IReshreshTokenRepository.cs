// <copyright file="IReshreshTokenRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

public interface IRefreshTokenRepository
{
    void Add(RefreshToken token);

    RefreshToken? Get(string token);

    void Revoke(string token);
}
