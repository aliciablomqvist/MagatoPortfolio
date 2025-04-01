using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _service;

    public CollectionsController(ICollectionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Collection>>> GetAll()
    {
        var collections = await _service.GetAllCollectionsAsync();
        return Ok(collections);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Collection>> GetById(int id)
    {
        var collection = await _service.GetCollectionByIdAsync(id);
        return collection == null ? NotFound() : Ok(collection);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CollectionDto dto)
    {
        var collection = await _service.AddCollectionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = collection.Id }, collection);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CollectionDto dto)
    {
        var updated = await _service.UpdateCollectionAsync(id, dto);
        return updated ? Ok() : NotFound();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteCollectionAsync(id);
        return success ? NoContent() : NotFound();
    }

    //Colors
    [HttpPost("{id}/colors")]
    public async Task<IActionResult> AddColor(int id, [FromBody] ColorDto dto)
    {
        var success = await _service.AddColorAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPut("colors/{colorId}")]
    public async Task<IActionResult> UpdateColor(int colorId, [FromBody] ColorDto dto)
    {
        var success = await _service.UpdateColorAsync(colorId, dto);
        return success ? Ok() : NotFound();
    }

    [HttpDelete("colors/{colorId}")]
    public async Task<IActionResult> DeleteColor(int colorId)
    {
        var success = await _service.DeleteColorAsync(colorId);
        return success ? Ok() : NotFound();
    }

    //Materials
    [HttpPost("{id}/materials")]
    public async Task<IActionResult> AddMaterial(int id, [FromBody] MaterialDto dto)
    {
        var success = await _service.AddMaterialAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPut("materials/{materialId}")]
    public async Task<IActionResult> UpdateMaterial(int materialId, [FromBody] MaterialDto dto)
    {
        var success = await _service.UpdateMaterialAsync(materialId, dto);
        return success ? Ok() : NotFound();
    }

    [HttpDelete("materials/{materialId}")]
    public async Task<IActionResult> DeleteMaterial(int materialId)
    {
        var success = await _service.DeleteMaterialAsync(materialId);
        return success ? Ok() : NotFound();
    }

    // Sketches
    [HttpPost("{id}/sketches")]
    public async Task<IActionResult> AddSketch(int id, [FromBody] SketchDto dto)
    {
        var success = await _service.AddSketchAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPut("sketches/{sketchId}")]
    public async Task<IActionResult> UpdateSketch(int sketchId, [FromBody] SketchDto dto)
    {
        var success = await _service.UpdateSketchAsync(sketchId, dto);
        return success ? Ok() : NotFound();
    }

    [HttpDelete("sketches/{sketchId}")]
    public async Task<IActionResult> DeleteSketch(int sketchId)
    {
        var success = await _service.DeleteSketchAsync(sketchId);
        return success ? Ok() : NotFound();
    }
}
