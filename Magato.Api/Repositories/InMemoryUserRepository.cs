// <copyright file="InMemoryUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Models;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> users = new ();

    public void Add(User user)
    {
        user.Id = this.users.Count + 1;
        this.users.Add(user);
    }

    public User? GetByUsername(string username)
        => this.users.FirstOrDefault(u => u.Username == username);

    public User? GetAdmin()
        => this.users.FirstOrDefault(u => u.IsAdmin);

    public bool AdminExists()
        => this.users.Any(u => u.IsAdmin);

    public IEnumerable<User> GetAll() => this.users;
}
