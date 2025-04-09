using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;

namespace Magato.Api.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
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
            return Ok(new { user.Username, user.IsAdmin });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
