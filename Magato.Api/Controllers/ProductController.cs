using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Magato.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        _service.Get(id) is { } product ? Ok(product) : NotFound();

    [Authorize(Roles = "Admin")]

    [HttpPost]
    public IActionResult Create([FromBody] ProductDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _service.Add(dto);

        return CreatedAtAction(nameof(Get), new
        {
            id = dto.Id
        }, dto);
    }



    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, ProductDto dto)
    {
        if (id != dto.Id)
            return BadRequest();
        _service.Update(dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromServices] IFileStorageService fileStorage)
    {
        var url = await fileStorage.UploadAsync(file);
        return Ok(new
        {
            imageUrls = url
        });
    }
}
