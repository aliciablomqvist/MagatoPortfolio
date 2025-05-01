using Microsoft.AspNetCore.Mvc;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;
using Magato.Api.Data;
using Microsoft.AspNetCore.Authorization;

namespace Magato.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Send([FromBody] ContactMessageDto dto)
    {
        try
        {
            var result = await _contactService.HandleContactAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            //Loggning för felsök
            Console.WriteLine(" HÄR BLIR DET FEL: " + ex.Message);
            return StatusCode(500, "Internal Server Error");
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("messages")]

    public async Task<IActionResult> GetMessages()
    {
        var messages = await _contactService.GetAllMessagesAsync();
        return Ok(messages);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var success = await _contactService.DeleteMessageAsync(id);
        if (!success)
            return NotFound(new { error = "The message was not found." });

        return NoContent();
    }

}
