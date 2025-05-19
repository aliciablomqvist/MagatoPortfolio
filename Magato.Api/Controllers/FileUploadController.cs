// <copyright file="FileUploadController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment env;

    public UploadController(IWebHostEnvironment env)
{
        this.env = env;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
{
        if (file == null || file.Length == 0)
{
            return this.BadRequest("No file uploaded.");
        }

        var uploadsFolder = Path.Combine(this.env.WebRootPath ?? "wwwroot", "uploads");

        if (!Directory.Exists(uploadsFolder))
{
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, file.FileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var imageUrl = $"/uploads/{file.FileName}";
        return this.Ok(new
{
            imageUrl,
        });
    }
}
