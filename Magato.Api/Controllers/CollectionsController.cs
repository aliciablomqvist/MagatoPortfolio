// <copyright file="CollectionsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService service;

    public CollectionsController(ICollectionService service)
    {
        this.service = service;
    }

    // ----------------------
    // COLLECTIONS
    // ----------------------

    /// <summary>Gets all collections with related data.</summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Collection>>> GetAll()
    {
        var collections = await this.service.GetAllCollectionsAsync();
        return this.Ok(collections);
    }

    /// <summary>Gets a specific collection by ID.</summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Collection>> GetById(int id)
    {
        var collection = await this.service.GetCollectionByIdAsync(id);
        return collection == null ? this.NotFound() : this.Ok(collection);
    }

    /// <summary>Creates a new collection.</summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CollectionCreateDto dto)
    {
        var collection = await this.service.AddCollectionAsync(dto);
        return this.CreatedAtAction(nameof(this.GetById), new
        {
            id = collection.Id,
        }, collection);
    }

    /// <summary>Updates a collection by ID.</summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CollectionDto dto)
    {
        var updated = await this.service.UpdateCollectionAsync(id, dto);
        return updated ? this.Ok() : this.NotFound();
    }

    /// <summary>Deletes a collection by ID.</summary>
    /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await this.service.DeleteCollectionAsync(id);
        return success ? this.NoContent() : this.NotFound();
    }

    // ----------------------
    // COLORS
    // ----------------------
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/colors")]
    public async Task<IActionResult> AddColor(int id, [FromBody] ColorDto dto)
    {
        var success = await this.service.AddColorAsync(id, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("colors/{colorId}")]
    public async Task<IActionResult> UpdateColor(int colorId, [FromBody] ColorDto dto)
    {
        var success = await this.service.UpdateColorAsync(colorId, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("colors/{colorId}")]
    public async Task<IActionResult> DeleteColor(int colorId)
    {
        var success = await this.service.DeleteColorAsync(colorId);
        return success ? this.Ok() : this.NotFound();
    }

    // ----------------------
    // MATERIALS
    // ----------------------
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/materials")]
    public async Task<IActionResult> AddMaterial(int id, [FromBody] MaterialDto dto)
    {
        var success = await this.service.AddMaterialAsync(id, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("materials/{materialId}")]
    public async Task<IActionResult> UpdateMaterial(int materialId, [FromBody] MaterialDto dto)
    {
        var success = await this.service.UpdateMaterialAsync(materialId, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("materials/{materialId}")]
    public async Task<IActionResult> DeleteMaterial(int materialId)
    {
        var success = await this.service.DeleteMaterialAsync(materialId);
        return success ? this.Ok() : this.NotFound();
    }

    // ----------------------
    // SKETCHES
    // ----------------------
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/sketches")]
    public async Task<IActionResult> AddSketch(int id, [FromBody] SketchDto dto)
    {
        var success = await this.service.AddSketchAsync(id, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("sketches/{sketchId}")]
    public async Task<IActionResult> UpdateSketch(int sketchId, [FromBody] SketchDto dto)
    {
        var success = await this.service.UpdateSketchAsync(sketchId, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("sketches/{sketchId}")]
    public async Task<IActionResult> DeleteSketch(int sketchId)
    {
        var success = await this.service.DeleteSketchAsync(sketchId);
        return success ? this.Ok() : this.NotFound();
    }

    // ----------------------
    // LOOKBOOK
    // ----------------------
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/lookbook")]
    public async Task<IActionResult> AddLookbookImage(int id, [FromBody] LookbookImageDto dto)
    {
        var success = await this.service.AddLookbookImageAsync(id, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("lookbook/{imageId}")]
    public async Task<IActionResult> UpdateLookbookImage(int imageId, [FromBody] LookbookImageDto dto)
    {
        var success = await this.service.UpdateLookbookImageAsync(imageId, dto);
        return success ? this.Ok() : this.NotFound();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("lookbook/{imageId}")]
    public async Task<IActionResult> DeleteLookbookImage(int imageId)
    {
        var success = await this.service.DeleteLookbookImageAsync(imageId);
        return success ? this.Ok() : this.NotFound();
    }
}
