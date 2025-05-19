// <copyright file="IFileStorageService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file);
}
