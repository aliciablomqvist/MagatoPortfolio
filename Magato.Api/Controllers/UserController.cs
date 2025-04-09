using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;
using Microsoft.AspNetCore.Authorization;


namespace Magato.Api.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register(UserRegisterDto dto)
    {
        try
        {
            var user = _userService.RegisterAdmin(dto);
            return Ok(new { user.Username, user.IsAdmin });
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

            return Ok(new
            {
                token,
                user.Username,
                user.IsAdmin
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

//Testa gömd admin endpoint
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminSecret()
    {
        return Ok("🎉 DU HAR ÅTKOMST 🎉");
    }

}
