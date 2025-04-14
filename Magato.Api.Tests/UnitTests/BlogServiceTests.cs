
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;
using Magato.Api.Models;
using Magato.Api.DTO;
using Magato.Api.Repositories;
using Magato.Api.Services;

namespace Magato.Api.Tests.UnitTests;

public class BlogServiceTests
{
    private readonly BlogPostService _service;
    private readonly Mock<IBlogPostRepository> _repoMock;

    public BlogServiceTests()
    {
        _repoMock = new Mock<IBlogPostRepository>();
        _service = new BlogPostService(_repoMock.Object);
    }

    [Fact]
    public void GetAll_Returns_All_Blogs()
    {
        var blogs = new List<BlogPost>
        {
            new BlogPost { Id = 1, Title = "First", Content = "Hello" },
            new BlogPost { Id = 2, Title = "Second", Content = "World" }
        };
        _repoMock.Setup(r => r.GetAll()).Returns(blogs);

        var result = _service.GetAll();

        result.Should().HaveCount(2);
    }

    [Fact]
    public void Add_Calls_Repo_Add()
    {
        var dto = new BlogPostDto { Title = "New", Content = "Test" };
        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<BlogPost>(b => b.Title == "New" && b.Content == "Test")), Times.Once);
    }

    [Fact]
    public void Get_Returns_Null_When_Post_Not_Found()
    {
        var repoMock = new Mock<IBlogPostRepository>();
        repoMock.Setup(r => r.Get(It.IsAny<int>())).Returns((BlogPost?)null);

        var service = new BlogPostService(repoMock.Object);

        var result = service.Get(123);

        result.Should().BeNull();
    }
    [Fact]
    public void GetAll_Returns_Empty_List_When_No_Posts()
    {
        var repoMock = new Mock<IBlogPostRepository>();
        repoMock.Setup(r => r.GetAll()).Returns(new List<BlogPost>());

        var service = new BlogPostService(repoMock.Object);

        var result = service.GetAll();

        result.Should().BeEmpty();
    }
    [Fact]
    public void Add_Calls_Repo_Add_With_Correct_Values()
    {
        var repoMock = new Mock<IBlogPostRepository>();
        var service = new BlogPostService(repoMock.Object);

        var dto = new BlogPostDto
        {
            Id = 0,
            Title = "Test Title",
            Content = "Some content",
            Author = "Author A",
            PublishedAt = DateTime.UtcNow,
            Tags = new List<string> { "test", "fun" },
            ImageUrls = new List<string> { "https://img.com/test.png" }
        };

        service.Add(dto);

        repoMock.Verify(r => r.Add(It.Is<BlogPost>(p =>
            p.Title == dto.Title &&
            p.Content == dto.Content &&
            p.Author == dto.Author &&
            p.Slug == "test-title" &&
            p.Tags.Contains("test") &&
            p.ImageUrls.Contains("https://img.com/test.png")
        )), Times.Once);
    }

    [Fact]
    public void Update_Calls_Repo_Update_With_All_Fields()
    {
        var dto = new BlogPostDto
        {
            Id = 1,
            Title = "Updated title",
            Content = "Updated content",
            Author = "Author",
            PublishedAt = DateTime.UtcNow,
            Slug = "updated-post",
            Tags = new List<string> { "test", "update" },
            ImageUrls = new List<string> { "https://example.com/image.jpg" }
        };

        var repoMock = new Mock<IBlogPostRepository>();
        var service = new BlogPostService(repoMock.Object);

        BlogPost? passedPost = null;

        repoMock.Setup(r => r.Update(It.IsAny<BlogPost>()))
                .Callback<BlogPost>(b => passedPost = b);

        service.Update(dto);

        passedPost.Should().NotBeNull();
        passedPost!.Id.Should().Be(dto.Id);
        passedPost.Slug.Should().Be(dto.Slug);
        passedPost.Tags.Should().BeEquivalentTo(dto.Tags);
        passedPost.ImageUrls.Should().BeEquivalentTo(dto.ImageUrls);

        repoMock.Verify(r => r.Update(It.IsAny<BlogPost>()), Times.Once);
    }


    [Fact]
    public void Delete_Calls_Repo_Delete()
    {
        var repoMock = new Mock<IBlogPostRepository>();
        var service = new BlogPostService(repoMock.Object);

        service.Delete(5);

        repoMock.Verify(r => r.Delete(5), Times.Once);
    }

}
