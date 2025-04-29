using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Magato.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthIntegrationTests(WebApplicationFactory<Program> factory)
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
                    options.UseInMemoryDatabase("AuthTestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Register_And_Login_Admin_Success()
    {
        var registerDto = new UserRegisterDto { Username = "admin", Password = "admin123" };
        var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerDto);

        if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
        {
            var registerContent = await registerResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Register Response (400): " + registerContent);

            registerContent.Should().Contain("Admin already exists");
        }
        else
        {
            registerResponse.EnsureSuccessStatusCode();
        }

        var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
        loginResponse.EnsureSuccessStatusCode();

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>();
        loginResult.Should().NotBeNull();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult!.Token);

        var adminResponse = await _client.GetAsync("/api/auth/admin-only");
        adminResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await adminResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Admin route says: " + content);
    }

    [Fact]
    public async Task Admin_Only_Endpoint_Should_Return_401_When_Unauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;

        var response = await _client.GetAsync("/api/auth/admin-only");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Unauthorized access response: " + content);
    }

    [Fact]
    public async Task Admin_Only_Endpoint_Should_Return_401_When_Token_Is_Invalid()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid.token.value");

        var response = await _client.GetAsync("/api/auth/admin-only");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Invalid token response: " + content);
    }

    [Theory]
    [InlineData("", "password123")]
    [InlineData("admin", "")]
    [InlineData("", "")]
    [InlineData("user", "123")]
    public async Task Register_Fails_With_Invalid_Input(string username, string password)
    {
        var dto = new UserRegisterDto { Username = username, Password = password };
        var response = await _client.PostAsJsonAsync("/api/auth/register", dto);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Validation error: " + content);

        content.Should().Contain("errors");
    }

    [Fact]
    public async Task Second_Admin_Registration_Should_Fail()
    {
        var dto1 = new UserRegisterDto { Username = "admin", Password = "admin123" };
        var dto2 = new UserRegisterDto { Username = "admin2", Password = "admin456" };

        await _client.PostAsJsonAsync("/api/auth/register", dto1);
        var secondResponse = await _client.PostAsJsonAsync("/api/auth/register", dto2);

        Assert.False(secondResponse.IsSuccessStatusCode);
    }
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
