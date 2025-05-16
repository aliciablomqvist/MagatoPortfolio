// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
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
