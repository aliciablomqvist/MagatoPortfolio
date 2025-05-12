// <copyright file="ContactController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Data;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService contactService;

    public ContactController(IContactService contactService)
    {
        this.contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactMessageDto dto)
    {
        try
        {
            var result = await this.contactService.HandleContactAsync(dto);

            if (!result.IsSuccess)
            {
                return this.BadRequest(new
                {
                    errors = result.Errors,
                });
            }

            return this.Ok(new
            {
                success = true,
            });
        }
        catch (Exception ex)
        {
            // Loggning för felsök
            Console.WriteLine(" HÄR BLIR DET FEL: " + ex.Message);
            return this.StatusCode(500, "Internal Server Error");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("messages")]

    public async Task<IActionResult> GetMessages()
    {
        var messages = await this.contactService.GetAllMessagesAsync();
        return this.Ok(messages);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var success = await this.contactService.DeleteMessageAsync(id);
        if (!success)
        {
            return this.NotFound(new
            {
                error = "The message was not found.",
            });
        }

        return this.NoContent();
    }
}
