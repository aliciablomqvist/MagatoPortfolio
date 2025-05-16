
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
        var dto = new UserRegisterDto{ Username = "admin", Password = "password123" };
        var user = _service.RegisterAdmin(dto);

        Assert.NotNull(user);
        Assert.True(user.IsAdmin);
        Assert.Equal("admin", user.Username);
    }

    [Fact]
    public void RegisterAdmin_Fails_WhenAdminAlreadyExists()
{
        _service.RegisterAdmin(new UserRegisterDto{ Username = "admin", Password = "password123" });

        var ex = Assert.Throws<InvalidOperationException>(() =>
            _service.RegisterAdmin(new UserRegisterDto{ Username = "admin2", Password = "newpass" })
        );

        Assert.Equal("Admin already exists", ex.Message);
    }

    [Fact]
    public void Authenticate_Succeeds_WithCorrectCredentials()
{
        _service.RegisterAdmin(new UserRegisterDto{ Username = "admin", Password = "password123" });

        var user = _service.Authenticate(new UserLoginDto{ Username = "admin", Password = "password123" });

        Assert.NotNull(user);
        Assert.Equal("admin", user.Username);
    }

    [Fact]
    public void Authenticate_Fails_WithWrongPassword()
{
        _service.RegisterAdmin(new UserRegisterDto{ Username = "admin", Password = "password123" });

        var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            _service.Authenticate(new UserLoginDto{ Username = "admin", Password = "wrongpass" })
        );

        Assert.Equal("Wrong username or password", ex.Message);
    }

    [Fact]
    public void Authenticate_Fails_WithUnknownUser()
{
        var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            _service.Authenticate(new UserLoginDto{ Username = "nobody", Password = "doesntmatter" })
        );

        Assert.Equal("Wrong username or password", ex.Message);
    }


    [Fact]
    public void AdminExists_ReturnsTrue_WhenAdminCreated()
{
        Assert.False(_repo.AdminExists());
        _service.RegisterAdmin(new UserRegisterDto{ Username = "admin", Password = "password123" });
        Assert.True(_repo.AdminExists());
    }
}
