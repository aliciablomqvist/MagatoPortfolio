// <copyright file="ContactController.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
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
    public async Task<IActionResult> Send([FromBody] ContactMessageCreateDto dto)
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
            Console.WriteLine("CHECK THIS ERROR: " + ex.Message);
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
