using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Magato.Api.Services;
using Magato.Api.DTO;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/cms")]
public class CmsController : ControllerBase
{
    private readonly IPageContentService _service;

    public CmsController(IPageContentService service)
    {
        _service = service;
    }

    [HttpGet("{key}")]
    public IActionResult GetContent(string key)
    {
        var content = _service.Get(key);
        return content == null ? NotFound() : Ok(content);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateContent([FromBody] PageContentDto dto)
    {
        _service.Add(dto);
        return Created($"/api/cms/{dto.Key}", dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{key}")]
    public IActionResult UpdateContent(string key, PageContentDto dto)
    {
        if (key != dto.Key)
            return BadRequest();

        _service.Update(dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{key}")]
    public IActionResult DeleteContent(string key)
    {
        var existing = _service.Get(key);
        if (existing == null)
            return NotFound();

        _service.Delete(key);
        return NoContent();
    }
}
