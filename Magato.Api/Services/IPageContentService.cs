// <copyright file="IPageContentService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;

public interface IPageContentService
{
    PageContentDto? Get(string key);

    IEnumerable<PageContentDto> GetAll();

    void Update(PageContentDto dto);

    void Add(PageContentDto dto);

    void Delete(string key);
}
