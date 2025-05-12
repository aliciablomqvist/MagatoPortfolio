// <copyright file="LookBookDTO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.DTO
{
    /// <summary>
    /// Represents an image in the collection's lookbook.
    /// </summary>
    public class LookbookImageDto
    {
        public string Url { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
