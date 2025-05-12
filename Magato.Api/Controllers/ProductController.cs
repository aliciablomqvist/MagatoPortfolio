// <copyright file="ProductController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    private readonly IProductService service;

    public ProductsController(IProductService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => this.Ok(this.service.GetAll());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        this.service.Get(id) is { } product ? this.Ok(product) : this.NotFound();

    [Authorize(Roles = "Admin")]

    [HttpPost]
    public IActionResult Create([FromBody] ProductDto dto)
    {
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        this.service.Add(dto);

        return this.CreatedAtAction(nameof(this.Get), new
        {
            id = dto.Id,
        }, dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, ProductDto dto)
    {
        if (id != dto.Id)
        {
            return this.BadRequest();
        }

        this.service.Update(dto);
        return this.NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        this.service.Delete(id);
        return this.NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromServices] IFileStorageService fileStorage)
    {
        var url = await fileStorage.UploadAsync(file);
        return this.Ok(new
        {
            imageUrls = url,
        });
    }
}
