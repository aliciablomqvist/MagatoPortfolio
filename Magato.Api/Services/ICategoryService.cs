// <copyright file="ICategoryService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
public interface ICategoryService
{
    IEnumerable<CategoryDto> GetAll();

    CategoryDto? GetById(int id);

    void Add(CategoryDto dto);

    void Update(int id, CategoryDto dto);

    void Delete(int id);
}
