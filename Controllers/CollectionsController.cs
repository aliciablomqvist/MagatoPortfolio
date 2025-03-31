using Microsoft.AspNetCore.Mvc;
using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CollectionsController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Collection>>> Get() =>
             await _context.Collections.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Collection>> Get(int id)
    {
        var collection = await _context.Collections.FindAsync(id);
        return collection == null ? NotFound() : Ok(collection);
    }

    [HttpPost]
    public async Task<ActionResult<Collection>> Post(Collection collection)
    {
        _context.Collections.Add(collection);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = collection.Id }, collection);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Collection collection)
    {
        if (id != collection.Id) return BadRequest();
        _context.Entry(collection).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/sketches")]
    public async Task<IActionResult> AddSketch(int id, [FromBody] SketchDto dto)
    {
        var success = await _service.AddSketchAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPost("{id}/materials")]
    public async Task<IActionResult> AddMaterial(int id, [FromBody] MaterialDto dto)
    {
        var success = await _service.AddMaterialAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPost("{id}/colors")]
    public async Task<IActionResult> AddColor(int id, [FromBody] ColorDto dto)
    {
        var success = await _service.AddColorAsync(id, dto);
        return success ? Ok() : NotFound();
    }

    [HttpPut("colors/{colorId}")]
    public async Task<IActionResult> UpdateColor(int colorId, [FromBody] ColorOptionDto dto)
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
