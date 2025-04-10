using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GdprController : ControllerBase
{
    private readonly IConfiguration _config;

    public GdprController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("contact-text")]
    public IActionResult GetContactGdprText()
    {
        var text = _config["Gdpr:ContactFormText"] ??
                   "Ingen GDPR-text är definierad.";
        return Ok(new { text });
    }
}
