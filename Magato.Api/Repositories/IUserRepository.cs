// <copyright file="IUserRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Repositories;
using Magato.Api.Models;

public interface IUserRepository
{
    User? GetByUsername(string username);

    User? GetAdmin();

    void Add(User user);

    bool AdminExists();

    IEnumerable<User> GetAll();
}
