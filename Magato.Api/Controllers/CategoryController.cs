using Microsoft.AspNetCore.Mvc;
using Magato.Api.Services;
using Magato.Api.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(CategoryDto dto)
    {
        _service.Add(dto);
        return CreatedAtAction(nameof(GetAll), null);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, CategoryDto dto)
    {
        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new
            {
                message = $"Category with ID {id} not found."
            });
        }

        _service.Update(id, dto);
        return NoContent(); 
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _service.GetById(id);
        if (existing == null)
        {
            return NotFound(new
            {
                message = $"Category with ID {id} not found."
            });
        }

        _service.Delete(id);
        return NoContent();
    }
}
