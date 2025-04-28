using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/inquiries")]
public class ProductInquiryController : ControllerBase
{
    private readonly IProductInquiryService _service;

    public ProductInquiryController(IProductInquiryService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create(ProductInquiryDto dto)
    {
        var response = _service.Add(dto);
        return Created("", new
        {
            message = "Thank you for your inquiry! We will get back to you as soon as possible.",
            inquiry = response
        });
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var inquiry = _service.GetById(id);
        return inquiry == null ? NotFound() : Ok(inquiry);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}/handle")]
    public IActionResult MarkAsHandled(int id)
    {
        _service.MarkAsHandled(id);
        return NoContent();
    }
}
