// <copyright file="GDPRController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
