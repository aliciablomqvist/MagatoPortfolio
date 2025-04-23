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
}

//Lägg till fler senare (PUT och DELETE)
