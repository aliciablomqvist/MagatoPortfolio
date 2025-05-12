// <copyright file="PageContentController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/cms")]
public class CmsController : ControllerBase
{
    private readonly IPageContentService service;

    public CmsController(IPageContentService service)
    {
        this.service = service;
    }

    [HttpGet("{key}")]
    public IActionResult GetContent(string key)
    {
        var content = this.service.Get(key);
        return content == null ? this.NotFound() : this.Ok(content);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(this.service.GetAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateContent([FromBody] PageContentDto dto)
    {
        this.service.Add(dto);
        return this.Created($"/api/cms/{dto.Key}", dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{key}")]
    public IActionResult UpdateContent(string key, PageContentDto dto)
    {
        if (key != dto.Key)
        {
            return this.BadRequest();
        }

        this.service.Update(dto);
        return this.NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{key}")]
    public IActionResult DeleteContent(string key)
    {
        var existing = this.service.Get(key);
        if (existing == null)
        {
            return this.NotFound();
        }

        this.service.Delete(key);
        return this.NoContent();
    }

    // Enpoint för att ladda upp filer
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromServices] IFileStorageService fileStorage)
    {
        var url = await fileStorage.UploadAsync(file);
        return this.Ok(new
        {
            mediaUrl = url, // ändra till image url eller lös på annat dätt
        });
    }
}
