// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.Models;

namespace Magato.Api.Repositories;
public interface IUserRepository
{
    User? GetByUsername(string username);

    User? GetAdmin();

    void Add(User user);

    bool AdminExists();

    IEnumerable<User> GetAll();
}
