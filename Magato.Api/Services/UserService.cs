using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Validators;
using Magato.Api.Repositories;
using System.Security.Cryptography;
using System.Text;
namespace Magato.Api.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public User RegisterAdmin(UserRegisterDto dto)
    {

        if (_repo.AdminExists())
            throw new InvalidOperationException("Admin already exists");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = Hash(dto.Password),
            IsAdmin = true
        };

        _repo.Add(user);
        return user;
    }

    public User Authenticate(UserLoginDto dto)
    {

        var user = _repo.GetByUsername(dto.Username);
        if (user == null || user.PasswordHash != Hash(dto.Password))
            throw new UnauthorizedAccessException("Wrong username or password");

        return user;
    }


    private string Hash(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public User GetByUsername(string username)
    {
        return _repo.GetByUsername(username)
               ?? throw new Exception("User not found");
    }
}
