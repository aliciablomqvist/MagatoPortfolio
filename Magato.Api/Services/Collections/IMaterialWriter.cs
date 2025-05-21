// <copyright file="IMaterialWriter.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>

using Magato.Api.DTO;

public interface IMaterialWriter
{
    // Materials
    Task<bool> AddAsync(int collectionId, MaterialDto dto);

    Task<bool> UpdateAsync(int materialId, MaterialDto dto);

    Task<bool> DeleteAsync(int materialId);
}
