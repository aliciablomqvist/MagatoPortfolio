
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
        var content = new PageContent
        {
            Key = "AboutMe",
            Title = "About Me Title",
            MainText = "Main Text",
            ExtraText = "Additional",
            Published = true,
            LastModified = DateTime.UtcNow,
            ImageUrls = new List<string> { "url1.jpg" }
        };

        _repoMock.Setup(r => r.Get("AboutMe")).Returns(content);

        var result = _service.Get("AboutMe");

        result.Should().NotBeNull();
        result!.Key.Should().Be("AboutMe");
        result.Title.Should().Be("About Me Title");
        result.MainText.Should().Be("Main Text");
        result.ExtraText.Should().Be("Additional");
        result.Published.Should().BeTrue();
        result.ImageUrls.Should().Contain("url1.jpg");
    }

    [Fact]
    public void Get_Returns_Null_When_Not_Found()
    {
        _repoMock.Setup(r => r.Get("Missing")).Returns((PageContent?)null);

        var result = _service.Get("Missing");

        result.Should().BeNull();
    }

    [Fact]
    public void GetAll_Returns_All_Content()
    {
        var list = new List<PageContent>
        {
            new PageContent { Key = "AboutMe", Title = "A", MainText = "Text A" },
            new PageContent { Key = "StartPage", Title = "B", MainText = "Text B" }
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
        var dto = new PageContentDto
        {
            Key = "AboutMe",
            Title = "New Title",
            MainText = "Updated",
            ExtraText = "Extra",
            Published = true,
            ImageUrls = new List<string> { "img1.jpg" }
        };

        _repoMock.Setup(r => r.Get(dto.Key))
            .Returns(new PageContent { Key = "AboutMe", MainText = "Old" });

        _service.Update(dto);

        _repoMock.Verify(r => r.Update(It.Is<PageContent>(p =>
            p.Key == dto.Key &&
            p.Title == dto.Title &&
            p.MainText == dto.MainText &&
            p.ExtraText == dto.ExtraText &&
            p.Published == dto.Published &&
            p.ImageUrls.SequenceEqual(dto.ImageUrls)
        )), Times.Once);
    }

    [Fact]
    public void Create_Calls_Repo_Add()
    {
        var dto = new PageContentDto
        {
            Key = "NewPage",
            Title = "Title",
            MainText = "Text",
            ExtraText = "Extra",
            Published = true,
            ImageUrls = new List<string> { "img.jpg" }
        };

        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<PageContent>(p =>
            p.Key == dto.Key &&
            p.Title == dto.Title &&
            p.MainText == dto.MainText &&
            p.ExtraText == dto.ExtraText &&
            p.Published == dto.Published &&
            p.ImageUrls.SequenceEqual(dto.ImageUrls)
        )), Times.Once);
    }

    [Fact]
    public void Delete_Calls_Repo_Delete()
    {
        _service.Delete("AboutMe");

        _repoMock.Verify(r => r.Delete("AboutMe"), Times.Once);
    }
}
