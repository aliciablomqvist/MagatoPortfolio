// <copyright file="UserController.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
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

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminSecret()
    {
        return this.Ok("🎉 YOU HAVE ACCESS! 🎉");
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
