namespace Magato.Api.DTO
{

    /// <summary>
    /// Represents the data required to create a new collection.
    /// </summary>
    public class CollectionCreateDto
    {
        public string CollectionTitle { get; set; } = "";
        public string CollectionDescription { get; set; } = "";
        public DateTime ReleaseDate { get; set; }

        public List<ColorDto> Colors { get; set; } = new();
        public List<MaterialDto> Materials { get; set; } = new();
        public List<SketchDto> Sketches { get; set; } = new();
    }
}