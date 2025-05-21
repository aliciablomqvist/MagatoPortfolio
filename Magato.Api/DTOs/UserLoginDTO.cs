// <copyright file="UserLoginDTO.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.DTO;
public class UserLoginDto
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
