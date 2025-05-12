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
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(IUserService userService, ITokenService tokenService, IRefreshTokenService refreshTokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _refreshTokenService = refreshTokenService;
    }

    //Temporär endpoint för att registera admin
    [HttpPost("register")]
    public IActionResult Register(UserRegisterDto dto)
    {
        try
        {
            var user = _userService.RegisterAdmin(dto);
            return Ok(new
            {
                user.Username,
                user.IsAdmin
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        try
        {
            var user = _userService.Authenticate(dto);
            var token = _tokenService.GenerateToken(user);
            var refreshToken = _refreshTokenService.CreateAndStore(user.Username);

            return Ok(new
            {
                token,
                refreshToken = refreshToken.Token,
                user.Username,
                user.IsAdmin
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    //Testa gömd admin endpoint - ta bort sen
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminSecret()
    {
        return Ok("🎉 DU HAR ÅTKOMST 🎉");
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        var stored = _refreshTokenService.Get(refreshToken);
        if (stored == null || stored.IsRevoked || stored.Expires < DateTime.UtcNow)
            return Unauthorized("Invalid refresh token");

        var user = _userService.GetByUsername(stored.Username);
        var newAccessToken = _tokenService.GenerateToken(user);

        return Ok(new
        {
            token = newAccessToken
        });
    }
}
