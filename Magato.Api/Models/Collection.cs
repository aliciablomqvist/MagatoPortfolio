// <copyright file="Collection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Models;

// Model for Collection
public class Collection
{
    public int Id
{
        get; set;
    }

    required public string CollectionTitle
{
        get; set;
    }

    required public string CollectionDescription
{
        get; set;
    }

    public DateTime ReleaseDate
{
        get; set;
    }

    public List<LookbookImage> LookbookImages{ get; set; } = new ();

    public List<ColorOption> Colors{ get; set; } = new ();

    public List<Material> Materials{ get; set; } = new ();

    public List<Sketch> Sketches{ get; set; } = new ();

    public ICollection<Product> Products{ get; set; } = new List<Product>();
}
