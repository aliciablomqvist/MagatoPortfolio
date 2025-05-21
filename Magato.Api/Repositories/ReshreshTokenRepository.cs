// <copyright file="ReshreshTokenRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Add(RefreshToken token)
    {
        this.context.RefreshTokens.Add(token);
        this.context.SaveChanges();
    }

    public RefreshToken? Get(string token)
    {
        return this.context.RefreshTokens.FirstOrDefault(t => t.Token == token);
    }

    public void Revoke(string token)
    {
        var refresh = this.Get(token);
        if (refresh != null)
        {
            refresh.IsRevoked = true;
            this.context.SaveChanges();
        }
    }
}
