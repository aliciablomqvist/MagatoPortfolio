// <copyright file="LookBook.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Magato.Api.Models;

using System.Text.Json.Serialization;

public class LookbookImage
{
    public int Id
    {
        get; set;
    }

    public string Url { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int CollectionId
    {
        get; set;
    }

    [JsonIgnore]
    public Collection? Collection
    {
        get; set;
    }
}
