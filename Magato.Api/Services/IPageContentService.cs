// <copyright file="IPageContentService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;

namespace Magato.Api.Services;

public interface IPageContentService
{
    PageContentDto? Get(string key);

    IEnumerable<PageContentDto> GetAll();

    void Update(PageContentDto dto);

    void Add(PageContentDto dto);

    void Delete(string key);
}
