

namespace Magato.Api.DTO
{
    public class CollectionDto
    {
        public string CollectionTitle { get; set; } = "";
        public string CollectionDescription { get; set; } = "";
        public DateTime ReleaseDate { get; set; }
       /* public List<ColorOptionDto>? Colors { get; set; }
        public List<MaterialDto>? Materials { get; set; }
        public List<SketchDto>? Sketches { get; set; }*/
    }
}
