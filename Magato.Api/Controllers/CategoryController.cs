// <copyright file="CategoryController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;
using Magato.Api.DTO;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService service;

    public CategoriesController(ICategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return this.Ok(this.service.GetAll());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(CategoryDto dto)
    {
        this.service.Add(dto);
        return this.CreatedAtAction(nameof(this.GetAll), null);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, CategoryDto dto)
    {
        var existing = this.service.GetById(id);
        if (existing == null)
        {
            return this.NotFound(new
            {
                message = $"Category with ID {id} not found.",
            });
        }

        this.service.Update(id, dto);
        return this.NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = this.service.GetById(id);
        if (existing == null)
        {
            return this.NotFound(new
            {
                message = $"Category with ID {id} not found.",
            });
        }

        this.service.Delete(id);
        return this.NoContent();
    }
}
