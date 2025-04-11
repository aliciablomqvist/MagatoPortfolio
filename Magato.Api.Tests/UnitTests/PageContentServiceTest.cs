using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

using Moq;

using Xunit;

namespace Magato.Api.Tests.UnitTests;

public class PageContentServiceTests
{
    private readonly PageContentService _service;
    private readonly Mock<IPageContentRepository> _repoMock;

    public PageContentServiceTests()
    {
        _repoMock = new Mock<IPageContentRepository>();
        _service = new PageContentService(_repoMock.Object);
    }

    [Fact]
    public void Get_Returns_Content_When_Found()
    {
        var content = new PageContent { Key = "AboutMe", Value = "Hello!" };
        _repoMock.Setup(r => r.Get("AboutMe"))
                 .Returns(new PageContent { Key = "AboutMe", Value = "Hello!" });

        var result = _service.Get("AboutMe");

        result.Should().NotBeNull();

        result!.Key.Should().Be("AboutMe");
        result.Value.Should().Be("Hello!");
    }

    [Fact]
    public void Get_Returns_Null_When_Not_Found()
    {
        _repoMock.Setup(r => r.Get("Missing"))!.Returns((PageContent?)null);

        var result = _service.Get("Missing");

        result.Should().BeNull();
    }

    [Fact]
    public void GetAll_Returns_All_Content()
    {
        var list = new List<PageContent>
        {
            new PageContent { Key = "AboutMe", Value = "Hej" },
            new PageContent { Key = "StartPage", Value = "Välkommen" }
        };
        _repoMock.Setup(r => r.GetAll()).Returns(list);

        var result = _service.GetAll().ToList();

        result.Should().HaveCount(2);
        result[0].Key.Should().Be("AboutMe");
        result[1].Key.Should().Be("StartPage");
    }

    [Fact]
    public void Update_Calls_Repo_Update()
    {
        var dto = new PageContentDto { Key = "AboutMe", Value = "Updated" };
        _repoMock.Setup(r => r.Get(dto.Key)).Returns(new PageContent { Key = "AboutMe", Value = "Old" });

        _service.Update(dto);

        _repoMock.Verify(r => r.Update(It.Is<PageContent>(p => p.Key == "AboutMe" && p.Value == "Updated")), Times.Once);
    }


    [Fact]
    public void Create_Calls_Repo_Add()
    {
        var dto = new PageContentDto { Key = "NewPage", Value = "New content" };

        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<PageContent>(p => p.Key == "NewPage" && p.Value == "New content")), Times.Once);
    }

    [Fact]
    public void Delete_Calls_Repo_Delete()
    {
        _service.Delete("AboutMe");

        _repoMock.Verify(r => r.Delete("AboutMe"), Times.Once);
    }
}
