// <copyright file="ReshreshTokenRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;

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
