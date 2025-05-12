namespace Magato.Api.DTO
{

    /// <summary>
    /// Represents a collection with full details
    /// </summary>
    public class CollectionDto
    {
        public int Id
        {
            get; set;
        }
        public string CollectionTitle { get; set; } = "";
        public string CollectionDescription { get; set; } = "";
        public DateTime ReleaseDate
        {
            get; set;
        }
        public List<ColorDto> Colors { get; set; } = new();
        public List<MaterialDto> Materials { get; set; } = new();
        public List<SketchDto> Sketches { get; set; } = new();
    }
}
