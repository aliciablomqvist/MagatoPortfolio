// <copyright file="UserService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services;
public class UserService : IUserService
{
    private readonly IUserRepository repo;

    public UserService(IUserRepository repo)
    {
        this.repo = repo;
    }

    public User RegisterAdmin(UserRegisterDto dto)
    {
        if (this.repo.AdminExists())
        {
            throw new InvalidOperationException("Admin already exists");
        }

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = this.Hash(dto.Password),
            IsAdmin = true,
        };

        this.repo.Add(user);
        return user;
    }

    public User Authenticate(UserLoginDto dto)
    {
        var user = this.repo.GetByUsername(dto.Username);
        if (user == null || user.PasswordHash != this.Hash(dto.Password))
        {
            throw new UnauthorizedAccessException("Wrong username or password");
        }

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
        return this.repo.GetByUsername(username)
               ?? throw new Exception("User not found");
    }
}
