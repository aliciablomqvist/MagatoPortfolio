// <copyright file="IFileStorageService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file);
}
