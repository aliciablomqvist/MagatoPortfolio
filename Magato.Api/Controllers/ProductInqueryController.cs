// <copyright file="ProductInqueryController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Controllers;

[ApiController]
[Route("api/inquiries")]
public class ProductInquiryController : ControllerBase
{
    private readonly IProductInquiryService service;

    public ProductInquiryController(IProductInquiryService service)
{
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductInquiryCreateDto dto)
{
        var response = await this.service.AddAsync(dto);
        return this.Created(string.Empty, new
{
            message = "Thank you for your inquiry! We will get back to you as soon as possible.",
            inquiry = response,
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
{
        var inquiries = await this.service.GetAllAsync();
        return this.Ok(inquiries);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
{
        var inquiry = await this.service.GetByIdAsync(id);
        return inquiry == null ? this.NotFound() : this.Ok(inquiry);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/handle")]
    public async Task<IActionResult> MarkAsHandled(int id)
{
        await this.service.MarkAsHandledAsync(id);
        return this.NoContent();
    }
}
