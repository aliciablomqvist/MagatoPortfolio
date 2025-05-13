using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

using Moq;


namespace Magato.Tests.UnitTests.Services;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _repoMock = new();
    private readonly ContactService _service;

    public ContactServiceTests()
    {
        _service = new ContactService(_repoMock.Object);
    }

    [Fact]
    public async Task HandleContactAsync_ReturnsSuccess_WhenInputIsValid()
    {
        var dto = new ContactMessageDto
        {
            Name = "Test",
            Email = "test@mail.com",
            Message = "Hejsvejs!",
            GdprConsent = true
        };

        var result = await _service.HandleContactAsync(dto);

        Assert.True(result.IsSuccess);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<ContactMessage>()), Times.Once);

    }

    [Fact]
    public async Task HandleContactAsync_ReturnsFailure_WhenValidationFails()
    {
        var dto = new ContactMessageDto
        {
            Name = "",
            Email = "felaktigmail",
            Message = ""
        };

        var result = await _service.HandleContactAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<ContactMessage>()), Times.Never);

    }
}
