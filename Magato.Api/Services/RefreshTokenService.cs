// <copyright file="RefreshTokenService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
using Magato.Api.Models;
using Magato.Api.Repositories;
public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository repo;

    public RefreshTokenService(IRefreshTokenRepository repo)
    {
        this.repo = repo;
    }

    public RefreshToken CreateAndStore(string username)
    {
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var refresh = new RefreshToken
        {
            Token = token,
            Username = username,
            Expires = DateTime.UtcNow.AddDays(7),
        };

        this.repo.Add(refresh);
        return refresh;
    }

    public RefreshToken? Get(string token) => this.repo.Get(token);

    public void Revoke(string token) => this.repo.Revoke(token);
}
