// <copyright file="PageContentController.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromServices] IFileStorageService fileStorage)
    {
        var url = await fileStorage.UploadAsync(file);
        return this.Ok(new
        {
            mediaUrl = url,
        });
    }
}
