using Microsoft.AspNetCore.Mvc;
using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

//Testing controller
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
}
