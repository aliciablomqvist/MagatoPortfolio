// <copyright file="RefreshToken.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Models;
public class RefreshToken
{
    public int Id
    {
        get; set;
    }

    public string Token { get; set; } = default!;

    public string Username { get; set; } = default!;

    public DateTime Expires
    {
        get; set;
    }

    public bool IsRevoked
    {
        get; set;
    }

    public DateTime Created { get; set; } = DateTime.UtcNow;
}
