using System.Collections.Generic;
using System.Threading.Tasks;
using Magato.Api.DTO;
using Magato.Api.Controllers;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;
using Moq;
using Xunit;
using Magato.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
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

        if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
        {
            var registerContent = await registerResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Register Response (400): " + registerContent);

            registerContent.Should().Contain("Admin finns redan");
        }
        else
        {
            registerResponse.EnsureSuccessStatusCode(); 
        }

        var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
        loginResponse.EnsureSuccessStatusCode();

        var loginContent = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login success: " + loginContent);
    }

    [Theory]
    [InlineData("", "password123")]
    [InlineData("admin", "")]
    [InlineData("", "")]
    [InlineData("user", "123")] // kort lösenord
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
