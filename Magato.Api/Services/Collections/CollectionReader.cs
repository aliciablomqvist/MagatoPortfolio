// <copyright file="CollectionReader.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Services.Collections;

using Magato.Api.Models;
using Magato.Api.Repositories.Collections;

public sealed class CollectionReader : ICollectionReader
{
    private readonly ICollectionRepository repo;

    public CollectionReader(ICollectionRepository repo) => this.repo = repo;

    public Task<IEnumerable<Collection>> GetAllAsync() => this.repo.GetAllAsync();

    public Task<Collection?> GetByIdAsync(int id) => this.repo.GetByIdAsync(id);

    public Task<bool> ExistsAsync(int id) => this.repo.ExistsAsync(id);
}
