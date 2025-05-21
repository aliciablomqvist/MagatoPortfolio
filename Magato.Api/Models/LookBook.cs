// <copyright file="LookBook.cs" company="Magato">
// Copyright (c) Magato. All rights reserved.
// </copyright>
namespace Magato.Api.Models;

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
