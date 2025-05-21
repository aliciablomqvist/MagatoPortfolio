// <copyright file="IPageContentRepository.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Repositories;
using Magato.Api.Models;

public interface IPageContentRepository
{
    PageContent? Get(string key);

    IEnumerable<PageContent> GetAll();

    void Add(PageContent content);

    void Update(PageContent content);

    void Delete(string key);

    /*  public PageContent? GetByKey(string key)
{
          return Get(key);
      }*/
}
