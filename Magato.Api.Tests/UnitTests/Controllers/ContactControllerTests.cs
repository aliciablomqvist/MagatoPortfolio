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
using Magato.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;


namespace Magato.Tests.UnitTests.Controllers;
public class ContactControllerTests
{
    private readonly Mock<IContactService> _serviceMock = new();
    private readonly ContactController _controller;

    public ContactControllerTests()
    {
        _controller = new ContactController(_serviceMock.Object);
    }

    [Fact]
    public async Task Send_ReturnsOk_WhenServiceReturnsSuccess()
    {
        var dto = new ContactMessageDto
        {
            Name = "Test",
            Email = "test@mail.com",
            Message = "Hejsvejs!"
        };

        _serviceMock.Setup(s => s.HandleContactAsync(dto))
            .ReturnsAsync(Result.Success());

        var result = await _controller.Send(dto);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Send_ReturnsBadRequest_WhenServiceFails()
    {
        var dto = new ContactMessageDto
        {
            Name = "",
            Email = "test@mail.com",
            Message = ""
        };

        _serviceMock.Setup(s => s.HandleContactAsync(dto))
            .ReturnsAsync(Result.Failure(new List<string> { "Fel" }));

        var result = await _controller.Send(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
