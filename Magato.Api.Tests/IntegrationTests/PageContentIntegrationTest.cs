using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Services;
using Magato.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Magato.Api.Tests.IntegrationTests;

public class CmsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CmsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("environment", "Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestCmsDb"));

                var provider = services.BuildServiceProvider();
                using var scope = provider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                db.Users.RemoveRange(db.Users);
                db.PageContents.RemoveRange(db.PageContents);
                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123"))),
                    IsAdmin = true
                });
                db.PageContents.Add(new PageContent
                {
                    Key = "AboutMe",
                    Title = "About Me",
                    MainText = "Initial content",
                    ExtraText = "Extra info",
                    MediaUrls = new List<string> { "https://example.com/image1.jpg" },
                    Published = true,
                    LastModified = DateTime.UtcNow
                });
                db.SaveChanges();
            });
        }).CreateClient();
    }

    private async Task<string> LoginAndGetTokenAsync()
    {
        var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
        var content = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        return content!.Token;
    }

    [Fact]
    public async Task Get_PageContent_ByKey_Returns_Content()
    {
        var response = await _client.GetAsync("/api/cms/AboutMe");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<PageContentDto>();
        content.Should().NotBeNull();
        content!.MainText.Should().Be("Initial content");
        content.Title.Should().Be("About Me");
        content.Published.Should().BeTrue();
        content.MediaUrls.Should().Contain("https://example.com/image1.jpg");
    }

    [Fact]
    public async Task Get_All_PageContent_Returns_List()
    {
        var response = await _client.GetAsync("/api/cms");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var list = await response.Content.ReadFromJsonAsync<List<PageContentDto>>();
        list.Should().NotBeNull();
        list.Should().Contain(c => c.Key == "AboutMe");
    }

    [Fact]
    public async Task Update_PageContent_As_Admin_Returns_204()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dto = new PageContentDto
        {
            Key = "AboutMe",
            Title = "Updated Title",
            MainText = "Updated by test",
            ExtraText = "Updated extra",
            MediaUrls = new List<string> { "https://example.com/image2.jpg" },
            Published = false
        };

        var response = await _client.PutAsJsonAsync($"/api/cms/{dto.Key}", dto);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_PageContent_As_Anonymous_Returns_401()
    {
        var dto = new PageContentDto
        {
            Key = "AboutMe",
            Title = "Should Fail",
            MainText = "Anonymous attempt",
            MediaUrls = new List<string>()
        };

        var response = await _client.PutAsJsonAsync($"/api/cms/{dto.Key}", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_Nonexistent_PageContent_Returns_404()
    {
        var response = await _client.GetAsync("/api/cms/DoesNotExist");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Add_New_PageContent_As_Admin_Returns_201()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dto = new PageContentDto
        {
            Key = "NewPage",
            Title = "New Title",
            MainText = "This is new",
            ExtraText = "More info",
            MediaUrls = new List<string> { "https://example.com/new.jpg" },
            Published = true
        };

        var response = await _client.PostAsJsonAsync("/api/cms", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadFromJsonAsync<PageContentDto>();
        content.Should().NotBeNull();
        content!.Key.Should().Be("NewPage");
        content.Title.Should().Be("New Title");
        content.MediaUrls.Should().Contain("https://example.com/new.jpg");
    }

    [Fact]
    public async Task Delete_PageContent_As_Admin_Returns_204()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/cms/AboutMe");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Create_PageContent_As_Admin_Returns_Created()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dto = new PageContentDto
        {
            Key = "Welcome",
            Title = "Welcome Page",
            MainText = "Hello from test",
            MediaUrls = new List<string> { "https://example.com/welcome.jpg" },
            Published = true
        };

        var response = await _client.PostAsJsonAsync("/api/cms", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<PageContentDto>();
        created!.Key.Should().Be(dto.Key);
        created.Title.Should().Be("Welcome Page");
        created.MediaUrls.Should().Contain("https://example.com/welcome.jpg");
    }
}
