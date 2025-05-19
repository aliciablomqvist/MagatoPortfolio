// <copyright file="CollectionsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/collections")]
public sealed class CollectionsController : ControllerBase
{
    private readonly ICollectionReader reader;
    private readonly ICollectionWriter writer;
    private readonly IColorWriter colors;
    private readonly IMaterialWriter materials;
    private readonly ISketchWriter sketches;
    private readonly ILookbookWriter lookbooks;

    public CollectionsController(
        ICollectionReader reader,
        ICollectionWriter writer,
        IColorWriter colors,
        IMaterialWriter materials,
        ISketchWriter sketches,
        ILookbookWriter lookbooks)
{
        this.reader = reader;
        this.writer = writer;
        this.colors = colors;
        this.materials = materials;
        this.sketches = sketches;
        this.lookbooks = lookbooks;
    }

    // ---------- Collection CRUD ----------
    [HttpGet]
    public Task<IEnumerable<Collection>> GetAll() => this.reader.GetAllAsync();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await this.reader.GetByIdAsync(id)) is{ } col ? this.Ok(col) : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CollectionCreateDto dto)
{
        var created = await this.writer.AddAsync(dto);
        return this.CreatedAtAction(
            nameof(this.Get),
            new
{
                id = created.Id,
            },
            created);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CollectionDto dto)
        => await this.writer.UpdateAsync(id, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await this.writer.DeleteAsync(id) ? this.NoContent() : this.NotFound();

    // ---------- Colors ----------
    [Authorize(Roles = "Admin")]
    [HttpPost("{collectionId:int}/colors")]
    public async Task<IActionResult> AddColor(int collectionId, ColorDto dto)
        => await this.colors.AddAsync(collectionId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPut("colors/{colorId:int}")]
    public async Task<IActionResult> UpdateColor(int colorId, ColorDto dto)
        => await this.colors.UpdateAsync(colorId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("colors/{colorId:int}")]
    public async Task<IActionResult> DeleteColor(int colorId)
        => await this.colors.DeleteAsync(colorId) ? this.NoContent() : this.NotFound();

    // ---------- Materials ----------
    [Authorize(Roles = "Admin")]
    [HttpPost("{collectionId:int}/materials")]
    public async Task<IActionResult> AddMaterial(int collectionId, MaterialDto dto)
        => await this.materials.AddAsync(collectionId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPut("materials/{materialId:int}")]
    public async Task<IActionResult> UpdateMaterial(int materialId, MaterialDto dto)
        => await this.materials.UpdateAsync(materialId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("materials/{materialId:int}")]
    public async Task<IActionResult> DeleteMaterial(int materialId)
        => await this.materials.DeleteAsync(materialId) ? this.NoContent() : this.NotFound();

    // ---------- Sketches ----------
    [Authorize(Roles = "Admin")]
    [HttpPost("{collectionId:int}/sketches")]
    public async Task<IActionResult> AddSketch(int collectionId, SketchDto dto)
        => await this.sketches.AddAsync(collectionId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPut("sketches/{sketchId:int}")]
    public async Task<IActionResult> UpdateSketch(int sketchId, SketchDto dto)
        => await this.sketches.UpdateAsync(sketchId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("sketches/{sketchId:int}")]
    public async Task<IActionResult> DeleteSketch(int sketchId)
        => await this.sketches.DeleteAsync(sketchId) ? this.NoContent() : this.NotFound();

    // ---------- Lookbook images ----------
    [Authorize(Roles = "Admin")]
    [HttpPost("{collectionId:int}/lookbook")]
    public async Task<IActionResult> AddLookbookImage(int collectionId, LookbookImageDto dto)
        => await this.lookbooks.AddAsync(collectionId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPut("lookbook/{imageId:int}")]
    public async Task<IActionResult> UpdateLookbookImage(int imageId, LookbookImageDto dto)
        => await this.lookbooks.UpdateAsync(imageId, dto) ? this.NoContent() : this.NotFound();

    [Authorize(Roles = "Admin")]
    [HttpDelete("lookbook/{imageId:int}")]
    public async Task<IActionResult> DeleteLookbookImage(int imageId)
        => await this.lookbooks.DeleteAsync(imageId) ? this.NoContent() : this.NotFound();
}
