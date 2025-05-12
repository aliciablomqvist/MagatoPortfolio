// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly IRefreshTokenService refreshTokenService;

    public AuthController(IUserService userService, ITokenService tokenService, IRefreshTokenService refreshTokenService)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        this.refreshTokenService = refreshTokenService;
    }

    // Temporär endpoint för att registera admin
    [HttpPost("register")]
    public IActionResult Register(UserRegisterDto dto)
    {
        try
        {
            var user = this.userService.RegisterAdmin(dto);
            return this.Ok(new
            {
                user.Username,
                user.IsAdmin,
            });
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        try
        {
            var user = this.userService.Authenticate(dto);
            var token = this.tokenService.GenerateToken(user);
            var refreshToken = this.refreshTokenService.CreateAndStore(user.Username);

            return this.Ok(new
            {
                token,
                refreshToken = refreshToken.Token,
                user.Username,
                user.IsAdmin,
            });
        }
        catch (Exception ex)
        {
            return this.Unauthorized(ex.Message);
        }
    }

    // Testa gömd admin endpoint - ta bort sen
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminSecret()
    {
        return this.Ok("🎉 DU HAR ÅTKOMST 🎉");
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        var stored = this.refreshTokenService.Get(refreshToken);
        if (stored == null || stored.IsRevoked || stored.Expires < DateTime.UtcNow)
        {
            return this.Unauthorized("Invalid refresh token");
        }

        var user = this.userService.GetByUsername(stored.Username);
        var newAccessToken = this.tokenService.GenerateToken(user);

        return this.Ok(new
        {
            token = newAccessToken,
        });
    }
}
