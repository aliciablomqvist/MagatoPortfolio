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

                var emailMock = new Mock<IEmailService>();
                emailMock.Setup(e => e.SendContactNotificationAsync(It.IsAny<ContactMessageDto>()))
                         .Returns(Task.CompletedTask);
                services.AddSingleton(emailMock.Object);

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
                db.PageContents.Add(new PageContent { Key = "AboutMe", Value = "Initial content" });
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
        content!.Value.Should().Be("Initial content");
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

        var dto = new PageContentDto { Key = "AboutMe", Value = "Updated by test" };
        var response = await _client.PutAsJsonAsync($"/api/cms/{dto.Key}", dto);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_PageContent_As_Anonymous_Returns_401()
    {
        var dto = new PageContentDto { Key = "AboutMe", Value = "Should fail" };
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

        var dto = new PageContentDto { Key = "NewPage", Value = "This is new" };
        var response = await _client.PostAsJsonAsync("/api/cms", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var content = await response.Content.ReadFromJsonAsync<PageContentDto>();
        content.Should().NotBeNull();
        content!.Key.Should().Be("NewPage");
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

        var dto = new PageContentDto { Key = "Welcome", Value = "Hello from test" };
        var response = await _client.PostAsJsonAsync("/api/cms", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<PageContentDto>();
        created!.Key.Should().Be(dto.Key);
        created.Value.Should().Be(dto.Value);
    }
}
