// <copyright file="User.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Models;
public class User
{
    public int Id
    {
        get; set;
    }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsAdmin
    {
        get; set;
    }
}
