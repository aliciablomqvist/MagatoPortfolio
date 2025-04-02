using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;

//Model for Collection
public class Collection
{
    public int Id { get; set; }
    public required string CollectionTitle { get; set; }
    public required string CollectionDescription { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<LookbookImage> LookbookImages { get; set; } = new();
    public List<ColorOption> Colors { get; set; } = new();
    public List<Material> Materials { get; set; } = new();
    public List<Sketch> Sketches { get; set; } = new();
}