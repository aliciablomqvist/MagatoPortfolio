// <copyright file="BlogController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;

using Magato.Api.DTO;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/blog")]
public class BlogPostController : ControllerBase
{
    private readonly IBlogPostService service;

    public BlogPostController(IBlogPostService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
        => this.Ok(this.service.GetAll());

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var post = this.service.Get(id);
        return post == null ? this.NotFound() : this.Ok(post);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(BlogPostDto dto)
    {
        this.service.Add(dto);
        return this.CreatedAtAction(nameof(this.Get), new
        {
            id = dto.Id,
        }, dto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, BlogPostDto dto)
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

    [HttpGet("{slug}")]
    public IActionResult GetBySlug(string slug)
    {
        var post = this.service.GetBySlug(slug);
        return post == null ? this.NotFound() : this.Ok(post);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file, [FromServices] IFileStorageService fileStorage)
    {
        var url = await fileStorage.UploadAsync(file);
        return this.Ok(new
        {
            imageUrl = url,
        });
    }
}
