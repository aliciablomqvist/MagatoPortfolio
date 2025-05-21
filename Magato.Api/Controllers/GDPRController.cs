// <copyright file="GDPRController.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GdprController : ControllerBase
{
    private readonly IConfiguration config;

    public GdprController(IConfiguration config)
    {
        this.config = config;
    }

    [HttpGet("contact-text")]
    public IActionResult GetContactGdprText()
    {
        var text = this.config["Gdpr:ContactFormText"] ??
                   "No GDPR text found";
        return this.Ok(new
        {
            text,
        });
    }
}
