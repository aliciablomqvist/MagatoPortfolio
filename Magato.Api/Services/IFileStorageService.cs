// <copyright file="IFileStorageService.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file);
}
