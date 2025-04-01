

namespace Magato.Api.DTO
{
    public class CollectionDto
    {
        public int Id { get; set; } // Ska detta avara med här eller skapa ny DTO?
        public string CollectionTitle { get; set; } = "";
        public string CollectionDescription { get; set; } = "";
        public DateTime ReleaseDate { get; set; }

        public List<ColorDto> Colors { get; set; } = new();
        public List<MaterialDto> Materials { get; set; } = new();
        public List<SketchDto> Sketches { get; set; } = new();
    }
}
