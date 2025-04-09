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
public class AuthUnitTests
{
    private readonly IUserService _service;
    private readonly IUserRepository _repo;

    public AuthUnitTests()
    {
        _repo = new InMemoryUserRepository();
        _service = new UserService(_repo);
    }

    [Fact]
    public void RegisterAdmin_Succeeds_WhenNoAdmin()
    {
        var dto = new UserRegisterDto { Username = "admin", Password = "password123" };
        var user = _service.RegisterAdmin(dto);

        Assert.NotNull(user);
        Assert.True(user.IsAdmin);
        Assert.Equal("admin", user.Username);
    }

    [Fact]
    public void RegisterAdmin_Fails_WhenAdminAlreadyExists()
    {
        _service.RegisterAdmin(new UserRegisterDto { Username = "admin", Password = "password123" });

        var ex = Assert.Throws<InvalidOperationException>(() =>
            _service.RegisterAdmin(new UserRegisterDto { Username = "admin2", Password = "newpass" })
        );

        Assert.Equal("Admin finns redan", ex.Message);
    }

    [Fact]
    public void Authenticate_Succeeds_WithCorrectCredentials()
    {
        _service.RegisterAdmin(new UserRegisterDto { Username = "admin", Password = "password123" });

        var user = _service.Authenticate(new UserLoginDto { Username = "admin", Password = "password123" });

        Assert.NotNull(user);
        Assert.Equal("admin", user.Username);
    }

    [Fact]
    public void Authenticate_Fails_WithWrongPassword()
    {
        _service.RegisterAdmin(new UserRegisterDto { Username = "admin", Password = "password123" });

        var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            _service.Authenticate(new UserLoginDto { Username = "admin", Password = "wrongpass" })
        );

        Assert.Equal("Fel användarnamn eller lösenord", ex.Message);
    }

    [Fact]
    public void Authenticate_Fails_WithUnknownUser()
    {
        var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            _service.Authenticate(new UserLoginDto { Username = "nobody", Password = "doesntmatter" })
        );

        Assert.Equal("Fel användarnamn eller lösenord", ex.Message);
    }


    [Fact]
    public void AdminExists_ReturnsTrue_WhenAdminCreated()
    {
        Assert.False(_repo.AdminExists());
        _service.RegisterAdmin(new UserRegisterDto { Username = "admin", Password = "password123" });
        Assert.True(_repo.AdminExists());
    }
}
