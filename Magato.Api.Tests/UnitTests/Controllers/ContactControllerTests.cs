using System.Collections.Generic;
using System.Threading.Tasks;
using Magato.Api.DTO;
using Magato.Api.Controllers;
using Magato.Api.Models;
using Magato.Api.Data;
using Magato.Api.Repositories;
using Magato.Api.Services;
using Moq;
using Xunit;
using Magato.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace Magato.Tests.UnitTests.Controllers;
    public class ContactControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly ContactController _controller;

    public ContactControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _controller = new ContactController(_context);
    }

    [Fact]
    public async Task SendMessage_ReturnsOk_ForValidInput()
    {

        var dto = new ContactMessageDto
        {
            Name = "Testperson",
            Email = "test@mail.com",
            Message = "Hejsvejs!"
        };


        var result = await _controller.SendMessage(dto);


        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(1, _context.ContactMessages.Count());
    }

    [Fact]
    public async Task SendMessage_ReturnsBadRequest_ForMissingName()
    {
        var dto = new ContactMessageDto
        {
            Email = "test@mail.com",
            Message = "Hejsvejs!"
        };

        _controller.ModelState.AddModelError("Name", "Required");

        var result = await _controller.SendMessage(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
