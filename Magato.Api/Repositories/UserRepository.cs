// <copyright file="UserRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Data;
using Magato.Api.Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;

    public UserRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Add(User user)
    {
        this.context.Users.Add(user);
        this.context.SaveChanges();
    }

    public User? GetByUsername(string username)
    {
        return this.context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetAdmin()
    {
        return this.context.Users.FirstOrDefault(u => u.IsAdmin);
    }

    public bool AdminExists()
    {
        return this.context.Users.Any(u => u.IsAdmin);
    }

    public IEnumerable<User> GetAll()
    {
        return this.context.Users.ToList();
    }
}
