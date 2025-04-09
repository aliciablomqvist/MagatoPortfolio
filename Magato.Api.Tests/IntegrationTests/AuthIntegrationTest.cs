
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class AuthIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_And_Login_Admin_Success()
    {
        var registerDto = new UserRegisterDto { Username = "admin", Password = "admin123" };
        var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", registerDto);
        registerResponse.EnsureSuccessStatusCode();

        var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
        loginResponse.EnsureSuccessStatusCode();
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
