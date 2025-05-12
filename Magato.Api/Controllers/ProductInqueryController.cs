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
    public IActionResult Create(ProductInquiryDto dto)
    {
        var response = this.service.Add(dto);
        return this.Created(string.Empty, new
        {
            message = "Thank you for your inquiry! We will get back to you as soon as possible.",
            inquiry = response,
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAll() => this.Ok(this.service.GetAll());

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var inquiry = this.service.GetById(id);
        return inquiry == null ? this.NotFound() : this.Ok(inquiry);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/handle")]
    public IActionResult MarkAsHandled(int id)
    {
        this.service.MarkAsHandled(id);
        return this.NoContent();
    }
}
