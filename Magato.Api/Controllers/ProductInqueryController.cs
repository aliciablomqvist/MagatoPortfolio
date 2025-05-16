// <copyright file="ProductInqueryController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using Magato.Api.DTO;
using Magato.Api.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create(ProductInquiryDto dto)  // <-- ändrat till async Task<IActionResult>
    {
        var response = await this.service.AddAsync(dto);   // <-- await och Async
        return this.Created(string.Empty, new
        {
            message = "Thank you for your inquiry! We will get back to you as soon as possible.",
            inquiry = response,
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()  // <-- async
    {
        var inquiries = await this.service.GetAllAsync();  // <-- await
        return this.Ok(inquiries);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)   // <-- async
    {
        var inquiry = await this.service.GetByIdAsync(id);  // <-- await
        return inquiry == null ? this.NotFound() : this.Ok(inquiry);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/handle")]
    public async Task<IActionResult> MarkAsHandled(int id)  // <-- async
    {
        await this.service.MarkAsHandledAsync(id);   // <-- await
        return this.NoContent();
    }
}
