using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;
using Microsoft.AspNetCore.Authorization;

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

    // ----------------------
    // COLLECTIONS
    // ----------------------

    /// <summary>Gets all collections with related data.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Collection>>> GetAll()
    {
        var collections = await _service.GetAllCollectionsAsync();
        return Ok(collections);
    }

    /// <summary>Gets a specific collection by ID.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Collection>> GetById(int id)
    {
        var collection = await _service.GetCollectionByIdAsync(id);
        return collection == null ? NotFound() : Ok(collection);
    }

    /// <summary>Creates a new collection.</summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CollectionCreateDto dto)
    {
        var collection = await _service.AddCollectionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = collection.Id }, collection);
    }

    /// <summary>Updates a collection by ID.</summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CollectionDto dto)
    {
        var updated = await _service.UpdateCollectionAsync(id, dto);
        return updated ? Ok() : NotFound();
    }

    /// <summary>Deletes a collection by ID.</summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteCollectionAsync(id);
        return success ? NoContent() : NotFound();
    }

    // ----------------------
    // COLORS
    // ----------------------
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/colors")]
    public async Task<IActionResult> AddColor(int id, [FromBody] ColorDto dto)
    {
        var success = await _service.AddColorAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("colors/{colorId}")]
    public async Task<IActionResult> UpdateColor(int colorId, [FromBody] ColorDto dto)
    {
        var success = await _service.UpdateColorAsync(colorId, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("colors/{colorId}")]
    public async Task<IActionResult> DeleteColor(int colorId)
    {
        var success = await _service.DeleteColorAsync(colorId);
        return success ? Ok() : NotFound();
    }

    // ----------------------
    // MATERIALS
    // ----------------------

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/materials")]
    public async Task<IActionResult> AddMaterial(int id, [FromBody] MaterialDto dto)
    {
        var success = await _service.AddMaterialAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("materials/{materialId}")]
    public async Task<IActionResult> UpdateMaterial(int materialId, [FromBody] MaterialDto dto)
    {
        var success = await _service.UpdateMaterialAsync(materialId, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("materials/{materialId}")]
    public async Task<IActionResult> DeleteMaterial(int materialId)
    {
        var success = await _service.DeleteMaterialAsync(materialId);
        return success ? Ok() : NotFound();
    }

    // ----------------------
    // SKETCHES
    // ----------------------

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/sketches")]
    public async Task<IActionResult> AddSketch(int id, [FromBody] SketchDto dto)
    {
        var success = await _service.AddSketchAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("sketches/{sketchId}")]
    public async Task<IActionResult> UpdateSketch(int sketchId, [FromBody] SketchDto dto)
    {
        var success = await _service.UpdateSketchAsync(sketchId, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("sketches/{sketchId}")]
    public async Task<IActionResult> DeleteSketch(int sketchId)
    {
        var success = await _service.DeleteSketchAsync(sketchId);
        return success ? Ok() : NotFound();
    }

    // ----------------------
    // LOOKBOOK
    // ----------------------

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/lookbook")]
    public async Task<IActionResult> AddLookbookImage(int id, [FromBody] LookbookImageDto dto)
    {
        var success = await _service.AddLookbookImageAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("lookbook/{imageId}")]
    public async Task<IActionResult> UpdateLookbookImage(int imageId, [FromBody] LookbookImageDto dto)
    {
        var success = await _service.UpdateLookbookImageAsync(imageId, dto);
        return success ? Ok() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("lookbook/{imageId}")]
    public async Task<IActionResult> DeleteLookbookImage(int imageId)
    {
        var success = await _service.DeleteLookbookImageAsync(imageId);
        return success ? Ok() : NotFound();
    }
}
