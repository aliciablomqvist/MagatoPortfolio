using FluentAssertions;
using Magato.Api.Controllers;
using Magato.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Magato.Api.DTO;

namespace Magato.Api.Tests.UnitTests;

public class CmsControllerTests
{
    private readonly CmsController _controller;
    private readonly Mock<IPageContentService> _serviceMock;

    public CmsControllerTests()
    {
        _serviceMock = new Mock<IPageContentService>();
        _controller = new CmsController(_serviceMock.Object);
    }

    [Fact]
    public void GetContent_Returns_Ok_With_Content_When_Found()
    {
        var expected = new PageContentDto
        {
            Key = "AboutMe",
            Title = "Om mig",
            MainText = "Testinnehåll",
            ExtraText = "Extra",
            Published = true,
            ImageUrls = new List<string> { "image1.jpg" },
            LastModified = DateTime.UtcNow
        };
        _serviceMock.Setup(s => s.Get("AboutMe")).Returns(expected);

        var result = _controller.GetContent("AboutMe");

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        var content = okResult!.Value as PageContentDto;
        content!.Key.Should().Be("AboutMe");
        content.Title.Should().Be("Om mig");
    }

    [Fact]
    public void GetContent_Returns_NotFound_When_Null()
    {
        _serviceMock.Setup(s => s.Get("Missing")).Returns((PageContentDto?)null);

        var result = _controller.GetContent("Missing");

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void UpdateContent_Calls_Service_And_Returns_NoContent()
    {
        var dto = new PageContentDto
        {
            Key = "StartPage",
            Title = "Startsida",
            MainText = "Uppdaterad text",
            ExtraText = "Mer text",
            Published = false,
            ImageUrls = new List<string> { "a.jpg" },
            LastModified = DateTime.UtcNow
        };

        _serviceMock.Setup(s => s.Get(dto.Key)).Returns(dto);

        var result = _controller.UpdateContent(dto.Key, dto);

        _serviceMock.Verify(s => s.Update(dto), Times.Once);
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public void GetAll_Returns_Ok_With_List()
    {
        var list = new List<PageContentDto>
        {
            new PageContentDto { Key = "One", Title = "Ett", MainText = "A", ImageUrls = new(), LastModified = DateTime.UtcNow },
            new PageContentDto { Key = "Two", Title = "Två", MainText = "B", ImageUrls = new(), LastModified = DateTime.UtcNow }
        };

        _serviceMock.Setup(s => s.GetAll()).Returns(list);

        var result = _controller.GetAll();

        var ok = result as OkObjectResult;
        ok.Should().NotBeNull();
        var data = ok!.Value as IEnumerable<PageContentDto>;
        data.Should().HaveCount(2);
    }

    [Fact]
    public void CreateContent_Calls_Service_And_Returns_Created()
    {
        var dto = new PageContentDto
        {
            Key = "NewPage",
            Title = "Ny sida",
            MainText = "Nytt innehåll",
            ExtraText = "Mer info",
            Published = false,
            ImageUrls = new List<string> { "test.jpg" },
            LastModified = DateTime.UtcNow
        };

        var result = _controller.CreateContent(dto);

        _serviceMock.Verify(s => s.Add(dto), Times.Once);
        var created = result as CreatedResult;
        created.Should().NotBeNull();
        created!.Value.Should().BeEquivalentTo(dto);
        created.Location.Should().Contain(dto.Key);
    }

    [Fact]
    public void DeleteContent_Calls_Service_And_Returns_NoContent()
    {
        var existing = new PageContentDto { Key = "AboutMe", Title = "Om mig", MainText = "Info", ImageUrls = new(), LastModified = DateTime.UtcNow };
        _serviceMock.Setup(s => s.Get("AboutMe")).Returns(existing);

        var result = _controller.DeleteContent("AboutMe");

        _serviceMock.Verify(s => s.Delete("AboutMe"), Times.Once);
        result.Should().BeOfType<NoContentResult>();
    }
}
